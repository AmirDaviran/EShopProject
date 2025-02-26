using EShop.Domain.Entities.Account;
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.ViewModels.Products.Product;

namespace EShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task InsertAsync(Product product);
        Task<FilterProductViewModel> FilterAsync(FilterProductViewModel model);
        void Update(Product product);
        Task SaveAsync();

        //Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId); 
    }
}
