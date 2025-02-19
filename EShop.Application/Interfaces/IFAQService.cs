using EShop.Domain.Enums.FAQEnum;
using EShop.Domain.ViewModels.FAQ;

namespace EShop.Application.Interfaces
{
    public interface IFAQService
    {
        #region Admin Side

        Task<List<FAQViewModel>> GetAllAsync();

        Task<OperationResult> CreateAsync(CreateFAQViewModel model);

        Task<UpdateFAQViewModel> GetForUpdateAsync(int id);

        Task<OperationResult> UpdateAsync(UpdateFAQViewModel model);

        Task<OperationResult> DeleteAsync(int id);



        #endregion

        #region Client Side
        Task<List<FAQViewModel>> GetFAQAsync();
        #endregion

    }
}
