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
        [Required(ErrorMessage = "عنوان الزامی است")]
        [StringLength(150, ErrorMessage = "حداکثر ۱۵۰ کاراکتر")]
        public string Title { get; set; }

        [Required(ErrorMessage = "توضیحات کوتاه الزامی است")]
        [StringLength(300, ErrorMessage = "حداکثر ۳۰۰ کاراکتر")]
        public string TitleDescription { get; set; }

        [Required(ErrorMessage = "قیمت الزامی است")]
        [Range(1, int.MaxValue, ErrorMessage = "قیمت نامعتبر")]
        public int Price { get; set; }

        [StringLength(2000, ErrorMessage = "حداکثر ۲۰۰۰ کاراکتر")]
        public string? Review { get; set; }

        [StringLength(2000, ErrorMessage = "حداکثر ۲۰۰۰ کاراکتر")]
        public string? ExpertReview { get; set; }

        [Required(ErrorMessage = "تصویر محصول الزامی است")]
        public IFormFile ImageFile { get; set; }

    }
}
