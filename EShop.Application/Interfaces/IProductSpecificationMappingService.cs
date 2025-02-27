using EShop.Domain.Enums.ProductSpecificationMapping;
using EShop.Domain.ViewModels.Products.Product_Specification;

namespace EShop.Application.Interfaces
{
    public interface IProductSpecificationMappingService
    {
        Task<ProductSpecificationMappingResult> AddSpecificationToProductAsync(AddSpecificationToProductViewModel model);
        Task<List<ProductSpecificationListViewModel>> GetSpecificationsByProductIdAsync(int productId);
        Task<ProductSpecificationMappingResult> RemoveSpecificationFromProductAsync(int mappingId);
        Task<ProductSpecificationMappingResult> UpdateSpecificationAsync(int mappingId, AddSpecificationToProductViewModel model);
        Task<ProductSpecificationFilterViewModel> FilterAsync(ProductSpecificationFilterViewModel model);
    }
}