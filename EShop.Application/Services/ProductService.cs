using EShop.Application.Interfaces;
using EShop.Application.Utilities.Extensions.Upload;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Entities.ProductEntity.Mapping;
using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Products.Product;
using EShop.Domain.ViewModels.Products.Product.ClientSide;

namespace EShop.Application.Services
{
    public class ProductService : IProductService
    {
        #region Fields

        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductSpecificationMappingRepository _specMappingRepository;

        #endregion

        #region Constructor

        public ProductService(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IProductSpecificationMappingRepository specMappingRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _specMappingRepository = specMappingRepository;
        }

        #endregion


        #region Admin-Side

        #region Create

        public async Task<CreateProductResult> CreateAsync(CreateProductViewModel model)
        {
            // اعتبارسنجی اولیه
            if (model == null || string.IsNullOrWhiteSpace(model.Title) || model.Price <= 0 ||
                model.ImageFile == null || model.CategoryId <= 0)
            {
                return CreateProductResult.InvalidInput;
            }

            // بررسی اعتبار تصویر
            if (!model.ImageFile.IsImage())
            {
                return CreateProductResult.ImageUploadFailed;
            }

            // آپلود تصویر با استفاده از اکستنشن موجود
            var imageName = await model.ImageFile.SaveAttachmentAsync(SiteTools.ProducMainPicturetAttachmentsPath);

            if (string.IsNullOrEmpty(imageName))
            {
                return CreateProductResult.ImageUploadFailed;
            }

            // ایجاد موجودیت محصول
            var product = new Product
            {
                Title = model.Title,
                TitleDescription = model.TitleDescription,
                Price = model.Price,
                Review = model.Review,
                ExpertReview = model.ExpertReview,
                ImageName = imageName,
                CreatedDate = DateTime.Now,
                ProductCategoryMappings = new List<ProductCategoryMapping>
                {
                    new ProductCategoryMapping
                    {
                        CategoryId = model.CategoryId,
                        CreatedDate = DateTime.Now
                    } // کامنت: اضافه کردن دسته‌بندی
                }
            };
            // ذخیره در دیتابیس
            await _productRepository.InsertAsync(product);
            await _productRepository.SaveAsync();

            return CreateProductResult.Success;
        }

        #endregion

        #region Delete

        public async Task<DeleteProductResult> DeleteAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null || product.IsDeleted)
            {
                return DeleteProductResult.NotFound;
            }

            // حذف منطقی
            product.IsDeleted = true;

            // حذف فیزیکی تصویر
            if (!string.IsNullOrEmpty(product.ImageName))
            {
                product.ImageName.DeleteImage(SiteTools.ProducMainPicturetAttachmentsPath);
            }

            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return DeleteProductResult.Success;
        }

        #endregion

        #region GetAll

        public async Task<List<ProductListViewModel>> GetAllAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        #endregion

        #region GetForUpdate

        public async Task<UpdateProductViewModel> GetForUpdateAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return null;
            }
            else
            {
                return new UpdateProductViewModel()
                {
                    ExpertReview = product.ExpertReview,
                    Id = product.Id,
                    ImageName = product.ImageName,
                    Price = product.Price,
                    Review = product.Review,
                    Title = product.Title,
                    TitleDescription = product.TitleDescription,
                    CategoryId =
                        product.ProductCategoryMappings?.FirstOrDefault()?.CategoryId ??
                        0 // کامنت: گرفتن اولین دسته‌بندی
                    //    int categoryId = 0;
                    //    if (product.ProductCategoryMappings != null && product.ProductCategoryMappings.Any())
                    //    {
                    //    categoryId = product.ProductCategoryMappings.First().CategoryId;
                    //}
                };
            }
        }

        #endregion

        #region Update

        public async Task<UpdateProductResult> UpdateAsync(UpdateProductViewModel model)
        {
            // اعتبارسنجی اولیه
            if (model == null || model.Id <= 0 || string.IsNullOrWhiteSpace(model.Title) || model.Price <= 0 ||
                model.CategoryId <= 0)
                return UpdateProductResult.InvalidInput;

            // دریافت محصول موجود
            var product = await _productRepository.GetProductByIdAsync(model.Id);
            if (product == null)
            {
                return UpdateProductResult.NotFound;
            }

            // مدیریت تصویر
            if (model.ImageFile != null)
            {
                if (!model.ImageFile.IsImage())
                {
                    return UpdateProductResult.ImageUploadFailed;
                }

                // حذف تصویر قدیمی
                if (!string.IsNullOrEmpty(product.ImageName))
                {
                    product.ImageName.DeleteImage(SiteTools.ProducMainPicturetAttachmentsPath);
                }

                // آپلود تصویر جدید
                var newImageName =
                    await model.ImageFile.SaveAttachmentAsync(SiteTools.ProducMainPicturetAttachmentsPath);
                if (string.IsNullOrEmpty(newImageName))
                {
                    return UpdateProductResult.ImageUploadFailed;
                }

                product.ImageName = newImageName;
            }

            // آپدیت فیلدها
            product.Title = model.Title;
            product.TitleDescription = model.TitleDescription;
            product.Price = model.Price;
            product.Review = model.Review;
            product.ExpertReview = model.ExpertReview;

            //  به‌روزرسانی دسته‌بندی
            var existingCategory = product.ProductCategoryMappings?.FirstOrDefault();
            if (existingCategory != null)
            {
                existingCategory.CategoryId = model.CategoryId;
            }
            else
            {
                product.ProductCategoryMappings = new List<ProductCategoryMapping>
                {
                    new ProductCategoryMapping { CategoryId = model.CategoryId, CreatedDate = DateTime.Now }
                };
            }

            // ذخیره تغییرات
            _productRepository.Update(product);
            await _productRepository.SaveAsync();

            return UpdateProductResult.Success;
        }

        #endregion

        #region Filter

        public async Task<FilterProductViewModel> FilterAsync(FilterProductViewModel model)
        {
            return await _productRepository.FilterAsync(model);
        }

        #endregion


        #endregion

        #region Client Side

        #region GetMyProductSections
        public async Task<MyProductSectionsViewModel> GetMyProductSectionsAsync(int productId)
        {
            var product = await _productRepository.GetMyProductDataAsync(productId);
            if (product == null) return null;

            return new MyProductSectionsViewModel
            {
                ProductDetails = new ProductDetailsViewModel
                {
                    Title = product.Title,
                    TitleDescription = product.TitleDescription,
                    Price = product.Price,
                    ImageName = product.ImageName
                },
                ProductSpecifications = new ProductSpecificationsViewModel
                {
                    Specifications = product.ProductSpecificationMappings
                        .Select(psm => new SpecificationViewModel
                        {
                            Name = psm.Specification.Name,
                            Value = psm.Value
                        }).ToList()
                },
                ProductCategories = new ProductCategoriesViewModel
                {
                    Categories = product.ProductCategoryMappings
                        .Select(pcm => new CategoryViewModel
                        {
                            Title = pcm.Category.Title,
                            ParentCategoryId = pcm.Category.ParentCategoryId,
                            ParentCategoryTitle = pcm.Category.ParentCategory?.Title
                        }).ToList()
                },
                ProductReviews = new ProductReviewsViewModel
                {
                    Review = product.Review,
                    ExpertReview = product.ExpertReview
                }
            };
        }
        #endregion

        #region Filter
        public async Task<FilterClientSideProductViewModel> FilterClientAsync(FilterClientSideProductViewModel model)
        {
          return  await _productRepository.FilterClientAsync(model);
        }
        #endregion

        #endregion

    }
}
