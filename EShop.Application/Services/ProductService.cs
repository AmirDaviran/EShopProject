using EShop.Application.Interfaces;
using EShop.Application.Utilities.Extensions.Upload;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Enums;
using EShop.Domain.Enums.FAQEnum;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Products.Product;

namespace EShop.Application.Services
{
    public class ProductService(IProductRepository _productRepository) : IProductService
    {
        public async Task<CreateProductResult> CreateAsync(CreateProductViewModel model)
        {
            Product product = new Product()
            {
                CreatedDate = DateTime.Now,
                ExpertReview = model.ExpertReview,
                Price = model.Price,
                Review = model.Review,
                Title = model.Title,
                TitleDescription = model.TitleDescription,
            };

            if (model.ImageFile != null && model.ImageFile.Length > 0)
            {
                // استفاده از متد توسعه‌ای برای آپلود فایل
                var fileName = await model.ImageFile.SaveAttachmentAsync(SiteTools.ProducMainPicturetAttachmentsPath);
                if (fileName != null)
                {
                    //category.Icon = SiteTools.FAQCategoryAttachmentsPath + fileName;
                    product.ImageName = fileName; // تغییر اینجا: فقط fileName را استفاده بشه
                }
                else
                {
                    // در صورت ناموفق بودن آپلود می‌توان خطایی را مدیریت کرد
                    return CreateProductResult.Failure;
                }

            }
            await _productRepository.InsertAsync(product);
            await _productRepository.SaveAsync();
            return CreateProductResult.Success;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<UpdateProductViewModel> GetForUpdateAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateProductResult> UpdateAsync(UpdateProductViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
