using EShop.Domain.Enums.ContactUsEnums;
using EShop.Domain.ViewModels.ContactUsViewModel;

namespace EShop.Application.Interfaces;

public interface IContactUsService
{

    #region Admin

    Task<CreateContactUsResult> CreateAsync(CreateContactUsViewModel model);

    Task<FilterContactUsViewModel> FilterAsync(FilterContactUsViewModel filter);

    Task<ContactUsDetailsViewModel> GetDetailsByIdAsync(int id);

    Task<AnswerResult> AnswerAsync(ContactUsDetailsViewModel model);

    Task MarkAsReadAsync(int id);

    #endregion
}