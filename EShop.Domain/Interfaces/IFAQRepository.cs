using EShop.Domain.Entities.FAQ;
namespace EShop.Domain.Interfaces;
public interface IFAQRepository
{
    Task<List<FAQ>> GetAllFAQAsync();
    Task<FAQ> GetFAQByIdAsync(int id);
    Task InsertAsync(FAQ faqs);
    void Update(FAQ faqs);
    Task SaveAsync();

    #region Client Side
    
    Task<List<FAQ>> GetFAQsByCategoryIdAsync(int categoryId);

    #endregion
}