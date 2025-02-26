using EShop.Domain.Entities.ProductEntity.Mapping;

namespace EShop.Domain.Interfaces;
public interface IProductSpecificationMappingRepository
{
    Task<List<ProductSpecificationMapping>> GetByProductIdAsync(int productId);
    Task<ProductSpecificationMapping> GetByIdAsync(int id);
    Task InsertAsync(ProductSpecificationMapping mapping);
    void Update(ProductSpecificationMapping mapping);
    Task DeleteAsync(int id);
    Task SaveAsync();
}

