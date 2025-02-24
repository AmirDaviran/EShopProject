using EShop.Domain.Entities.ProductEntity;

namespace EShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(int id);
        Task InsertAsync(Product product);
        void Update(Product product);
        Task SaveAsync();
    }
}
