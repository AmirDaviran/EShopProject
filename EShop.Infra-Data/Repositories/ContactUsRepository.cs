using EShop.Domain.Entities.ContactUs;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories;

public class ContactUsRepository : IContactUsRepository
{
    #region Fields

    private readonly EShopDbContext _context;

    #endregion

    #region Constructor

    public ContactUsRepository(EShopDbContext context)
    {
        _context = context;
    }

    #endregion

    #region Methods

    public async Task InsertAsync(ContactUs contactUs)
    {
        await _context.ContactUs.AddAsync(contactUs);
    }

    public IQueryable<ContactUs> GetAll()
    {
        return _context.ContactUs.AsQueryable();
    }

    public async Task<ContactUs> GetByIdAsync(int id)
    {
        return await _context.ContactUs.FirstOrDefaultAsync(c => c.Id == id);
    }

    public void Update(ContactUs contactUs)
    {
        _context.ContactUs.Update(contactUs);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    #endregion
}