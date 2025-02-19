using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.FAQ
{
    public class UpdateFAQViewModel
    {
        public int Id { get; set; }

        [Display(Name = "سوال")]
        [Required(ErrorMessage = "لطفا {0} مورد نظر را بپرسید.")]
        public string Question { get; set; }


        [Display(Name = "جواب")]
        public string Answer { get; set; }

    }
}
