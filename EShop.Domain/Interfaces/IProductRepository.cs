using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.ViewModels.Products.Product;

namespace EShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        #region AdminSide

        Task<List<ProductListViewModel>> GetAllAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task InsertAsync(Product product);
        Task<FilterProductViewModel> FilterAsync(FilterProductViewModel model);
        void Update(Product product);
        Task SaveAsync();

        #endregion

        #region ClientSide

        Task<Product> GetMyProductDataAsync(int productId);

        #endregion
    }
}
