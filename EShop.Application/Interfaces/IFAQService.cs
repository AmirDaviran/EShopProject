using EShop.Domain.Entities.FAQ;
using EShop.Domain.Enums.FAQEnum;
using EShop.Domain.ViewModels.FAQ;

namespace EShop.Application.Interfaces;

public interface IFAQService
{
    #region AdminSide

    Task<List<FAQViewModel>> GetAllFAQForAdminAsync();
    Task<FAQUpdateViewModel> GetForUpdateAsync(int id);
    Task<OperationResult> CreateAsync(FAQCreateViewModel model);
    Task<OperationResult> UpdateAsync(FAQUpdateViewModel model);
    Task<OperationResult> DeleteAsync(int id);

    #endregion

    #region ClientSide

    Task<List<FAQViewModel>> GetAllFAQsAsync();

    #endregion
}