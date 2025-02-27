using EShop.Domain.Entities.ProductEntity.Mapping;
using EShop.Domain.ViewModels.Products.Product_Specification;

namespace EShop.Domain.Interfaces;
public interface IProductSpecificationMappingRepository
{
    Task<List<ProductSpecificationListViewModel>> GetByProductIdAsync(int productId);
    Task<ProductSpecificationMapping> GetByIdAsync(int id);
    Task InsertAsync(ProductSpecificationMapping mapping);
    void Update(ProductSpecificationMapping mapping);
    Task DeleteAsync(int id);
    Task SaveAsync();
}

