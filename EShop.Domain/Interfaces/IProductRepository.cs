using EShop.Domain.Entities.ProductEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProducts();
        Task<Product> GetProductById(int productId);
        Task AddProduct(Product product);
        Task<bool> DeleteProductById(int productId);
        Task UpdateProduct(Product product);



        #region Site Part
        Task<Product> ShowProductDetails(int productId);
        #endregion
        Task SaveChanges();
    }
}

