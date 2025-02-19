
using EShop.Domain.ViewModels.Common;

namespace EShop.Domain.ViewModels.ContactUsViewModel
{
    public class FilterContactUsViewModel : BasePaging<ContactUsDetailsViewModel>
    {
        public string? SearchTerm { get; set; }

    }
}
