using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories
{
    public class CategoryRepository(EShopDbContext _context) : ICategoryRepository
    {
        #region Category



        #region AddCategoryAsync
        public async Task AddCategoryAsync(Category category)
        {
            await _context.AddAsync(category);
        }
        #endregion

        #region GetAllCategoriesAsync
        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => !c.IsDeleted)
                .Include(c => c.ParentCategory)
                .Include(c => c.SubCategories)
                .ToListAsync();
        }
        #endregion

        #region GetCategoryByIdAsync
        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        #endregion

        #region IsCategoryExistAsync
        public async Task<bool> IsCategoryExistAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
        #endregion

        #region SoftDeleteCategoryAsync
        public async Task SoftDeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (category != null)
            {
                category.IsDeleted = true;
                _context.Categories.Update(category);
            }
        }
        #endregion

        #region UpdateCategoryAsync
        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
        }
        #endregion





        #endregion

        #region Product Categories




        #region AddProductSelectedCategories
        public async Task AddProductSelectedCategories(List<int> productSelectedCategories, int productId)
        {
            if (productSelectedCategories != null && productSelectedCategories.Any())
            {
                var newProductSelectedCategories = new List<ProductSelectedCategory>();

                foreach (var categoryId in productSelectedCategories)
                {
                    newProductSelectedCategories.Add(new ProductSelectedCategory
                    {
                        ProductId = productId,
                        CategoryId = categoryId
                    });
                }
                await _context.ProductSelectedCategories.AddRangeAsync(newProductSelectedCategories);
                await SaveChangesAsync();
            }
           
        }

        #endregion

        #region RemoveProductSelectedCategories
        public async Task RemoveProductSelectedCategories(int productId)
        {
            var allProductCategories = await _context.ProductSelectedCategories
                .Where(c => c.ProductId == productId).ToListAsync();
            if(allProductCategories != null)
            {
                _context.ProductSelectedCategories.RemoveRange(allProductCategories);
            }
        }

        #endregion

        #region GetAllProductCategoriesId
        public async Task<List<int>> GetAllProductCategoriesId(int productId)
        {
            return await _context.ProductSelectedCategories
                .Where(c => c.ProductId == productId)
                .Select(c => c.CategoryId)
                .ToListAsync();
        }

        #endregion





        #endregion

        #region GetAllProductCategoriesAsync
        public async Task<List<ProductSelectedCategory>> GetAllProductCategoriesAsync()
        {
            return await _context.ProductSelectedCategories
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }
        #endregion

        #region SaveChangesAsync
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        #endregion

    }
}
