using EShop.Domain.Entities.FAQ;
using EShop.Domain.ViewModels.FAQ;

namespace EShop.Domain.Interfaces;

public interface IFAQRepository
{
    Task<List<FAQs>> GetAllFAQAsync();
    Task<FAQs> GetFAQByIdAsync(int id);
    Task InsertAsync(FAQs faqs);
    void Update(FAQs faqs);
    Task SaveAsync();
}