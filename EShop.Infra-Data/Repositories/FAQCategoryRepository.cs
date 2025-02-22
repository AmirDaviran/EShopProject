using EShop.Domain.Entities.FAQ;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories;

public class FAQCategoryRepository(EShopDbContext _context): IFAQCategoryRepository
{
   
    public async Task<List<FAQCategory>> GetFAQCategoriesAsync()
    {
        return await _context.FAQCategories
            .Where(fc=>!fc.IsDeleted)
            .ToListAsync();
    }

    public async Task<FAQCategory> GetFAQCategoryByIdAsync(int id)
    {
        return await _context.FAQCategories
            .Where(fc => !fc.IsDeleted && fc.Id == id) // فیلتر درست اعمال شده
            .FirstOrDefaultAsync();
    }

   
    public async Task AddAsync(FAQCategory category)
    {
        await _context.FAQCategories.AddAsync(category);
    }

    public void Update(FAQCategory category)
    {
        _context.FAQCategories.Update(category);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}