using EShop.Domain.Entities.FAQ;

namespace EShop.Domain.Interfaces;

public interface IFAQCategoryRepository
{
    Task<List<FAQCategory>> GetFAQCategoriesAsync();
    Task<FAQCategory> GetFAQCategoryByIdAsync(int id);


     Task AddAsync(FAQCategory category);
    void Update(FAQCategory category);
     Task SaveAsync();
}