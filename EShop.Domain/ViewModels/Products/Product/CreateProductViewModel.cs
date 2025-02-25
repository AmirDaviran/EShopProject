using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Products.Product
{
    public class CreateProductViewModel
    {
        [Display(Name = "عنوان فارسی محصول ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string Title { get; set; }

        [Display(Name = "عنوان انگلیسی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string TitleDescription { get; set; }

        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }

        [Display(Name = "نقد و بررسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Review { get; set; }

        [Display(Name = "نقد و بررسی تخصصی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? ExpertReview { get; set; }

        [Required(ErrorMessage = "تصویر محصول الزامی است")]
        public IFormFile ImageFile { get; set; }

    }
}
