
using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories
{
    public class ProductRepository (EShopDbContext _context) : IProductRepository
    {

        #region Get All Products
        public async Task<List<Product>> GetAllProducts()
        {
            return await _context.Products
                .Where(p => !p.IsDeleted)
                .Include(p => p.ProductColorMappings)
                .ThenInclude(cm => cm.Color)
                .ToListAsync();
        }

        #endregion

        #region Get Product By Id
        public async Task<Product> GetProductById(int productId)
        {
            return await _context.Products
                .SingleOrDefaultAsync(p => p.Id == productId);
        }
        #endregion

        #region Add Product 
        public async Task AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
        }


        #endregion

        #region Update Product
        public async Task UpdateProduct(Product product)
        {
             _context.Products.Update(product);
        }

        #endregion

        #region Delete Product
        public async Task<bool> DeleteProductById(int productId)
        {
            var product = await GetProductById(productId);
            if (product != null)
            {
                product.IsDeleted = true;
               await UpdateProduct(product);
                await SaveChanges();
                return true;
            }

            return false;
        }

        #endregion

        #region Add Product Color Mapping
        //public async Task AddProductColorMappings(List<int> selectedColors, int amount, int productId)
        //{
        //    if (selectedColors != null && selectedColors.Any())
        //    {
        //        var newProductColorMappings = new List<ProductColorMapping>();
        //        foreach (var colorId in selectedColors)
        //        {
        //            newProductColorMappings.Add(new ProductColorMapping
        //            {
        //                ProductId = productId,
        //                ColorId = colorId,
        //                Amount = amount
        //            });
        //        }
        //        await _context.ProductColorMappings.AddRangeAsync(newProductColorMappings);
        //        await SaveChanges();
        //    }
        //}

        public async Task AddProductColorMapping(ProductColorMapping mapping)
        {
           await _context.ProductColorMappings.AddAsync(mapping);
        }


        #endregion

        #region Site Part
        public async Task<Product> ShowProductDetails(int productId)
        {
            return await _context.Products
                .Include(c => c.ProductSelectedCategories)
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync().ConfigureAwait(false);


        }
        #endregion

        #region SaveChanges
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

       

        #endregion

    }
}
