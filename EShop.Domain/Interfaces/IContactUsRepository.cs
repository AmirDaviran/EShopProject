using EShop.Domain.Entities.ContactUs;

namespace EShop.Domain.Interfaces;

public interface IContactUsRepository
{

    Task InsertAsync(ContactUs contactUs);

    IQueryable<ContactUs> GetAll();

    Task<ContactUs> GetByIdAsync(int id);

    void Update(ContactUs contactUs);

    Task SaveAsync();


}