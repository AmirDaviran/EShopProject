using EShop.Domain.Entities.FAQ;

namespace EShop.Application.Interfaces;

public interface IFAQCategoryService
{
    Task<List<FAQCategory>> GetFAQCategoriesAsync();
    Task<FAQCategory> GetFAQCategoryByIdAsync(int id);

    Task AddCategoryAsync(FAQCategory category);
    Task UpdateCategoryAsync(FAQCategory category);
    Task DeleteCategoryAsync(int id);
}