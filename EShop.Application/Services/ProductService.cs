using EShop.Application.Interfaces;
using EShop.Application.Utilities.Extensions.Upload;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Entities.ProductEntity.Mapping;
using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Products.ClientSide;
using EShop.Domain.ViewModels.Products.Product;
using static EShop.Application.Services.ProductService;

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
            if (model == null || string.IsNullOrWhiteSpace(model.Title) || model.Price <= 0 || model.ImageFile == null || model.CategoryId <= 0)
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
                    { CategoryId = model.CategoryId,
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
                    CategoryId = product.ProductCategoryMappings?.FirstOrDefault()?.CategoryId ?? 0 // کامنت: گرفتن اولین دسته‌بندی
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
            if (model == null || model.Id <= 0 || string.IsNullOrWhiteSpace(model.Title) || model.Price <= 0 || model.CategoryId <= 0)
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
                var newImageName = await model.ImageFile.SaveAttachmentAsync(SiteTools.ProducMainPicturetAttachmentsPath);
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

        #region GetProductsByCategoryId
        public async Task<List<ProductListViewModel>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _productRepository.GetProductsByCategoryIdAsync(categoryId);
        }
        #endregion

        #endregion

        #region Client Side

        #region GetProductDetail
        public async Task<ProductDetailViewModel> GetProductDetailAsync(int productId)
        {
            var product = await _productRepository.GetProductByIdAsync(productId);
            if (product == null || product.IsDeleted) return null;

            var specs = await _specMappingRepository.GetByProductIdAsync(productId);
            var category = product.ProductCategoryMappings?.FirstOrDefault()?.Category;

            return new ProductDetailViewModel
            {
                ProductId = product.Id,
                Title = product.Title,
                TitleDescription = product.TitleDescription,
                Price = product.Price,
                ImageUrl = @SiteTools.ProducMainPicturetAttachmentsPath,
                Review = product.Review,
                ExpertReview = product.ExpertReview,
                Variants = new List<ProductVariantViewModel>
                {
                    new ProductVariantViewModel { ColorName = "آبی", ColorCode = "#2196f3", IsSelected = true },
                    new ProductVariantViewModel { ColorName = "سفید", ColorCode = "#fff" },
                    new ProductVariantViewModel { ColorName = "صورتی", ColorCode = "#ff80ab" }
                },
                Specifications = specs.Select(s => new ProductSpecificationViewModel
                {
                    Name = s.SpecificationName,
                    Value = s.Value
                }).ToList(),
                Sellers = new List<SellerViewModel>
                {
                    new SellerViewModel
                    {
                        SellerName = "یکتاکالا",
                        Price = product.Price,
                        Warranty = "گارانتی ۱۸ ماهه",
                        SatisfactionRate = 88.4,
                        Stock = 1
                    }
                },
                AverageRating = 4.4,
                RatingCount = 742,
                CategoryTitle = category?.Title ?? "نامشخص"
            };
        }

        #endregion

        #region GetRelatedProducts

        public async Task<List<RelatedProductViewModel>> GetRelatedProductsAsync(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategoryIdAsync(categoryId);
            return products.Select(p => new RelatedProductViewModel
            {
                ProductId = p.Id,
                Title = p.Title,
                Price = p.Price,
                ImageUrl = @SiteTools.ProducMainPicturetAttachmentsPath,
                Rating = 4.4,
                RatingCount = 436,
                VariantColors = new List<string> { "#d4d4d4", "#e86841", "#b82c32" }
            }).Take(8).ToList();
        }

        #endregion


        #endregion
    }
}
