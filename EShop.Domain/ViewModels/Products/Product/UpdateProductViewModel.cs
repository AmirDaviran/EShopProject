﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.Products.Product
{
    public class UpdateProductViewModel
    {
        public int Id { get; set; }

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

        public string? ImageName { get; set; }
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "دسته‌بندی")]
        [Required(ErrorMessage = "لطفا {0} را انتخاب کنید")] // کامنت: اجباری کردن انتخاب دسته‌بندی
        public int CategoryId { get; set; }
    }
}
