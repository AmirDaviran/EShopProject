using System.ComponentModel.DataAnnotations;
using EShop.Domain.ViewModels.Common;

namespace EShop.Domain.ViewModels.UserViewModel
{
    public class FilterUserViewModel : BasePaging<UserDetailsViewModel>
    {
        [Display(Name = "نام")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }

        [Display(Name = "موبایل")]
        public string? Mobile { get; set; }

        [Display(Name = "ایمیل")]
        public string? Email { get; set; }

        [Display(Name = "فعال است؟")]
        public bool? IsActive { get; set; }

        public string? SearchTerm { get; set; } // فیلد جستجو

        public int TakeEntity { get; set; } = 10; // تعداد نمایش داده‌ها
    }
}
