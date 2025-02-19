using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.Account
{
    public class LoginViewModel
    {
        [MaxLength(300)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "گذرواژه")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(200, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }

    public enum LoginResult
    {
        NotFound,
        NotActive,
        InvalidCredentials,
        Success,
        IsBlocked
    }
}
