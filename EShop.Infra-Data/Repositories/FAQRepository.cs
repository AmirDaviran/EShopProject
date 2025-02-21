using EShop.Domain.Entities.FAQ;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories;

public class FAQRepository(EShopDbContext _context) : IFAQRepository
{

    #region GetAllFAQ
    public async Task<List<FAQs>> GetAllFAQAsync()
    {
        return await _context.FAQs
            .Where(f => !f.IsDeleted)
            .ToListAsync();
    }
    #endregion

    #region GetFAQById
    public async Task<FAQs> GetFAQByIdAsync(int id)
    {
        return await _context.FAQs
            .Where(f => f.Id == id && !f.IsDeleted)
            .FirstOrDefaultAsync();
    }
    #endregion

    #region Insert
    public async Task InsertAsync(FAQs faqs)
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
    public void Update(FAQs faqs)
    {
         _context.Update(faqs);
    }
    #endregion
}