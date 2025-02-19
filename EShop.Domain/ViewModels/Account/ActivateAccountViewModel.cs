using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Account
{
   public class ActivateAccountViewModel
    {

        [MaxLength(300)]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "کد فعال سازی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(20, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string ActiveCode { get; set; }
    }

    public enum ActivateAccountResult
    {
        Error,
        Success,
        NotFound
    }
}
