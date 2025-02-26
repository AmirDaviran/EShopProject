using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        #region Fields

        private readonly EShopDbContext _context;
        private readonly IProductRepository _productRepository;

        #endregion

        #region Constructor

        public CategoryRepository(EShopDbContext context, IProductRepository productRepository)
        {
            _context = context;
            _productRepository = productRepository;
        }

        #endregion

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

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion




        #endregion
    }
}
