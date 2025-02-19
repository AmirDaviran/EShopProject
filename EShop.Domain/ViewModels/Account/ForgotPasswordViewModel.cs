using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {

        [MaxLength(300)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        public string Code { get; set; }
    }

    public enum ForgotPasswordResult
    {
        Success,
        Failure,
        NotFound
    }
}
