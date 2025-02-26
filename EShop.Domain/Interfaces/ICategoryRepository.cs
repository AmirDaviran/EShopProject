using EShop.Domain.Entities.ProductEntity;

namespace EShop.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategoriesAsync();

        Task<Category> GetCategoryByIdAsync(int id);

        Task AddCategoryAsync (Category category);

        Task UpdateCategoryAsync (Category category);

        Task SoftDeleteCategoryAsync (int id);

        Task<bool> IsCategoryExistAsync(int id);

        Task SaveChangesAsync();



    }
}
