using EShop.Application.Interfaces;
using EShop.Domain.Entities.ProductEntity.Mapping;
using EShop.Domain.Enums.ProductSpecificationMapping;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Products.Product_Specification;

namespace EShop.Application.Services
{
    public class ProductSpecificationMappingService : IProductSpecificationMappingService
    {
        #region Fields

        private readonly IProductSpecificationMappingRepository _mappingRepository;
        private readonly IProductRepository _productRepository;
        private readonly ISpecificationRepository _specificationRepository;

        #endregion

        #region Constructor
        public ProductSpecificationMappingService(IProductSpecificationMappingRepository mappingRepository, IProductRepository productRepository, ISpecificationRepository specificationRepository)
        {
            _mappingRepository = mappingRepository;
            _productRepository = productRepository;
            _specificationRepository = specificationRepository;
        }

        #endregion

        public async Task<bool> AddSpecificationToProductAsync(AddSpecificationToProductViewModel model) // کامنت: تغییر به ویو مدل جدید
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Value))
                return false;

            var mapping = new ProductSpecificationMapping
            {
                ProductId = model.ProductId,
                SpecificationId = model.SpecificationId,
                Value = model.Value,
                CreatedDate = DateTime.Now
            };

            await _mappingRepository.InsertAsync(mapping);
            await _mappingRepository.SaveAsync();
            return true;
        }

        public async Task<List<ProductSpecificationListViewModel>> GetSpecificationsByProductIdAsync(int productId) // کامنت: تغییر به ویو مدل جدید
        {
            return await _mappingRepository.GetByProductIdAsync(productId);
        }

        public async Task<bool> RemoveSpecificationFromProductAsync(int mappingId)
        {
            await _mappingRepository.DeleteAsync(mappingId);
            await _mappingRepository.SaveAsync();
            return true;
        }

    }
}