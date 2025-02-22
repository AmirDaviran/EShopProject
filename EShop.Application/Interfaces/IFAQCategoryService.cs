using EShop.Domain.Entities.FAQ;
using EShop.Domain.Enums.FAQEnum;
using EShop.Domain.ViewModels.FAQCategory;

namespace EShop.Application.Interfaces;

public interface IFAQCategoryService
{
    Task<List<FAQCategory>> GetFAQCategoriesAsync();
    Task<FAQCategory> GetFAQCategoryByIdAsync(int id);
    Task<CreateFAQCategoryResult> CreateFAQCategoryAsync(FAQCategoryCreateViewModel createCategory);
    Task<UpdateFAQCategoryResult> UpdateFAQCategoryAsync(FAQCategoryUpdateViewModel updateCategory);
    Task<List<FAQCategory>> GetCategoriesOrderedAsync();
    Task DeleteCategoryAsync(int id);
}