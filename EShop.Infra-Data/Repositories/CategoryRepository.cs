using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories
{
    public class CategoryRepository(EShopDbContext _context) : ICategoryRepository
    {
        #region Methods

        #region Category
        public async Task AddCategoryAsync(Category category)
        {
            await _context.AddAsync(category);
        }

        public async Task<List<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => !c.IsDeleted)
                .Include(c => c.ParentCategory)
                .Include(c => c.SubCategories)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.ParentCategory)
                .Include(c => c.SubCategories)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> IsCategoryExistAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task SoftDeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

            if (category != null)
            {
                category.IsDeleted = true;

                _context.Categories.Update(category);
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _context.Categories.Update(category);
        }
        #endregion

        #region Product Categories

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

        public async Task RemoveProductSelectedCategories(int productId)
        {
            var allProductCategories = await _context.ProductSelectedCategories
                .Where(c => c.ProductId == productId).ToListAsync();
            if(allProductCategories != null)
            {
                _context.ProductSelectedCategories.RemoveRange(allProductCategories);
            }
        }

        public async Task<List<int>> GetAllProductCategoriesId(int productId)
        {
            return await _context.ProductSelectedCategories
                .Where(c => c.ProductId == productId)
                .Select(c => c.CategoryId)
                .ToListAsync();
        }

        #endregion

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductSelectedCategory>> GetAllProductCategoriesAsync()
        {
            return await _context.ProductSelectedCategories
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }


        #endregion
    }
}
