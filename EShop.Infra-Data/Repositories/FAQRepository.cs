using EShop.Domain.Entities.FAQ;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;
namespace EShop.Infra_Data.Repositories;
public class FAQRepository(EShopDbContext _context) : IFAQRepository
{
    #region AdminSide

    #region GetAllFAQ
    public async Task<List<FAQ>> GetAllFAQAsync()
    {
        return await _context.FAQs.Include(f => f.Category)
                        .Where(f => !f.IsDeleted)
                       .ToListAsync();
    }
    #endregion

    #region GetFAQById
    public async Task<FAQ> GetFAQByIdAsync(int id)
    {
        return await _context.FAQs
            .Where(f => f.Id == id && !f.IsDeleted)
            .FirstAsync();
    }
    #endregion

    #region Insert
    public async Task InsertAsync(FAQ faqs)
    {
        await _context.FAQs.AddAsync(faqs);
    }
    #endregion

    #region SaveAsync
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
    #endregion

    #region Update
    public void Update(FAQ faqs)
    {
        _context.Update(faqs);
    }
    #endregion


    #endregion

    #region ClientSide


    public async Task<List<FAQ>> GetFAQsByCategoryIdAsync(int categoryId)
    {
        return await _context.FAQs
            .Where(f => f.CategoryId == categoryId && !f.IsDeleted)
            .ToListAsync();
    }

    #endregion
}