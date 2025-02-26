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

        #region GetByProductId

        public async Task<List<SpecificationProductViewModel>> GetByProductIdAsync(int productId)
        {
            var mappings = await _mappingRepository.GetByProductIdAsync(productId);
            return mappings.Select(m => new SpecificationProductViewModel
            {
                SpecificationId = m.SpecificationId,
                Value = m.Value
            }).ToList();
        }

        #endregion

        #region Create

        public async Task<CreateProductSpecificationResult> CreateAsync(int productId, SpecificationProductViewModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Value) || model.SpecificationId <= 0)
                return CreateProductSpecificationResult.InvalidInput;

            var product = await _productRepository.GetProductByIdAsync(productId);
            var specification = await _specificationRepository.GetSpecificationByIdAsync(model.SpecificationId);
            if (product == null || specification == null)
                return CreateProductSpecificationResult.NotFound;

            var mapping = new ProductSpecificationMapping
            {
                ProductId = productId,
                SpecificationId = model.SpecificationId,
                Value = model.Value,
                CreatedDate = DateTime.Now
            };

            await _mappingRepository.InsertAsync(mapping);
            await _mappingRepository.SaveAsync();
            return CreateProductSpecificationResult.Success;
        }

        #endregion

        #region UpdateAsync

        public async Task<UpdateProductSpecificationResult> UpdateAsync(int mappingId, SpecificationProductViewModel model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Value) || model.SpecificationId <= 0)
                return UpdateProductSpecificationResult.InvalidInput;

            var mapping = await _mappingRepository.GetByIdAsync(mappingId);
            if (mapping == null)
                return UpdateProductSpecificationResult.NotFound;

            mapping.SpecificationId = model.SpecificationId;
            mapping.Value = model.Value;

            _mappingRepository.Update(mapping);
            await _mappingRepository.SaveAsync();
            return UpdateProductSpecificationResult.Success;
        }

        #endregion

        #region Delete

        public async Task<DeleteProductSpecificationResult> DeleteAsync(int mappingId)
        {
            var mapping = await _mappingRepository.GetByIdAsync(mappingId);
            if (mapping == null)
                return DeleteProductSpecificationResult.NotFound;

            await _mappingRepository.DeleteAsync(mappingId);
            await _mappingRepository.SaveAsync();
            return DeleteProductSpecificationResult.Success;
        }

        #endregion

    }
}