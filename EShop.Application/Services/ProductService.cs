using EShop.Application.Interfaces;
using EShop.Application.Utilities.Extensions.Upload;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Products.Product;

namespace EShop.Application.Services
{
    // استفاده از ساختار کلاسه جدید با پارامتر سازنده
    public class ProductService(IProductRepository _productRepository) : IProductService
    {
        #region Create

        public async Task<CreateProductResult> CreateAsync(CreateProductViewModel model)
        {
            // اعتبارسنجی اولیه
            if (model == null || string.IsNullOrWhiteSpace(model.Title) || model.Price <= 0 || model.ImageFile == null)
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
                ImageName = imageName
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
            var product = await _productRepository.GetByIdAsync(id);
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

        public async Task<List<ProductViewModel>> GetAllAsync()
        {
            var ticket = await _productRepository.GetAllAsync();
            return ticket.Select(t => new ProductViewModel()
            {
                CreatedDate = t.CreatedDate,
                ExpertReview = t.ExpertReview,
                Id = t.Id,
                ImageName = t.ImageName,
                Price = t.Price,
                Review = t.Review,
                Title = t.Title,
                TitleDescription = t.TitleDescription
            }).ToList();
        }

        #endregion

        #region GetForUpdate

        public async Task<UpdateProductViewModel> GetForUpdateAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
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
                    TitleDescription = product.TitleDescription
                };
            }
        }

        #endregion

        #region Update
        public async Task<UpdateProductResult> UpdateAsync(UpdateProductViewModel model)
        {
            // اعتبارسنجی اولیه
            if (model == null || model.Id <= 0)
            {
                return UpdateProductResult.InvalidInput;
            }

            // دریافت محصول موجود
            var product = await _productRepository.GetByIdAsync(model.Id);
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

            // ذخیره تغییرات
            _productRepository.Update(product);
             await _productRepository.SaveAsync();

             return UpdateProductResult.Success;
        }

        #endregion
    }
}
