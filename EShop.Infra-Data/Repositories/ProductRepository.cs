using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;


namespace EShop.Infra_Data.Repositories
{
    public class ProductRepository(EShopDbContext _contex) : IProductRepository
    {
        #region GetAll
        public async Task<List<Product>> GetAllAsync()
        {
            return await _contex.Products
                .Where(p=>!p.IsDeleted)
                .ToListAsync();
        }

        #endregion

                #region GetById
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _contex.Products
                 .Where(p => p.Id == id && !p.IsDeleted)
                 .FirstOrDefaultAsync();
        }

        #endregion

        #region Insert

        public async Task InsertAsync(Product product)
        {
            await _contex.Products.AddAsync(product);
        }

        #endregion

        #region SaveAsync
        public async Task SaveAsync()
        {
            await _contex.SaveChangesAsync();
        }

        #endregion

        #region Update

        public void Update(Product product)
        {
            _contex.Products.Update(product);
        }

        #endregion








    }
}
