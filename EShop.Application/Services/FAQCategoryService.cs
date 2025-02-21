using EShop.Application.Interfaces;
using EShop.Domain.Entities.FAQ;
using EShop.Domain.Interfaces;

namespace EShop.Application.Services;

public class FAQCategoryService(IFAQCategoryRepository _faqCategoryRepository) : IFAQCategoryService
{
    

    public async Task<List<FAQCategory>> GetFAQCategoriesAsync()
    {
        return await _faqCategoryRepository.GetFAQCategoriesAsync();
    }

    public async Task<FAQCategory> GetFAQCategoryByIdAsync(int id)
    {
        return await _faqCategoryRepository.GetFAQCategoryByIdAsync(id);
    }
    public async Task AddCategoryAsync(FAQCategory category)
    {
        await _faqCategoryRepository.AddAsync(category);
        await _faqCategoryRepository.SaveAsync();
    }

    public async Task UpdateCategoryAsync(FAQCategory category)
    {
        _faqCategoryRepository.Update(category);
        await _faqCategoryRepository.SaveAsync();
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await _faqCategoryRepository.GetFAQCategoryByIdAsync(id);
        if (category != null)
        {
            category.IsDeleted = true;
            await _faqCategoryRepository.SaveAsync();
        }
    }
}