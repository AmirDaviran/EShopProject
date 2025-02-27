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
        public ProductSpecificationMappingService(
            IProductSpecificationMappingRepository mappingRepository,
            IProductRepository productRepository,
            ISpecificationRepository specificationRepository)
        {
            _mappingRepository = mappingRepository;
            _productRepository = productRepository;
            _specificationRepository = specificationRepository;
        }
        #endregion

        #region AddSpecificationToProduct

        public async Task<ProductSpecificationMappingResult> AddSpecificationToProductAsync(AddSpecificationToProductViewModel model)
        {
            // اعتبارسنجی ورودی
            if (model == null || string.IsNullOrWhiteSpace(model.Value))
                return ProductSpecificationMappingResult.InvalidInput;

            var mapping = new ProductSpecificationMapping
            {
                ProductId = model.ProductId,
                SpecificationId = model.SpecificationId,
                Value = model.Value,
                CreatedDate = DateTime.Now
            };

            await _mappingRepository.InsertAsync(mapping);
            await _mappingRepository.SaveAsync();
            return ProductSpecificationMappingResult.Success;
        }

        #endregion

        #region GetSpecificationsByProductId

        public async Task<List<ProductSpecificationListViewModel>> GetSpecificationsByProductIdAsync(int productId)
        {
            return await _mappingRepository.GetByProductIdAsync(productId);
        }

        #endregion

        #region RemoveSpecificationFromProduct

        public async Task<ProductSpecificationMappingResult> RemoveSpecificationFromProductAsync(int mappingId)
        {
            var mapping = await _mappingRepository.GetByIdAsync(mappingId);
            if (mapping == null || mapping.IsDeleted)
                return ProductSpecificationMappingResult.NotFound;

            await _mappingRepository.DeleteAsync(mappingId);
            await _mappingRepository.SaveAsync();
            return ProductSpecificationMappingResult.Success;
        }

        #endregion

        #region UpdateSpecification

        public async Task<ProductSpecificationMappingResult> UpdateSpecificationAsync(int mappingId, AddSpecificationToProductViewModel model)
        {
            // اعتبارسنجی ورودی
            if (model == null || string.IsNullOrWhiteSpace(model.Value) || model.ProductId <= 0)
                return ProductSpecificationMappingResult.InvalidInput;

            var mapping = await _mappingRepository.GetByIdAsync(mappingId);
            if (mapping == null || mapping.IsDeleted)
                return ProductSpecificationMappingResult.NotFound;

            // آپدیت مشخصه
            mapping.SpecificationId = model.SpecificationId;
            mapping.Value = model.Value;

            _mappingRepository.Update(mapping);
            await _mappingRepository.SaveAsync();
            return ProductSpecificationMappingResult.Success;
        }

        #endregion

        #region Filter

        public async Task<ProductSpecificationFilterViewModel> FilterAsync(ProductSpecificationFilterViewModel model)
        {
            return await _mappingRepository.FilterAsync(model);
        }

        #endregion
    }
}