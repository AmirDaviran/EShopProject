using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Products
{
    public class EditProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "عنوان فارسی محصول ")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string PersianTitle { get; set; }

        [Display(Name = "عنوان انگلیسی محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(500, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
        public string EnglishTitle { get; set; }

        [Display(Name = "نقد و بررسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? Review { get; set; }

        [Display(Name = "نقد و بررسی تخصصی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string? ExpertReview { get; set; }
        [Display(Name = "قیمت محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int Price { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? ImageName { get; set; }
        public List<int> SelectedCategories { get; set; }
        public List<int> ProductColorMappings { get; set; }
    }
}
