using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.FAQCategory;

public class FAQCategoryCreateViewModel
{
    

    [Display(Name = "نام دسته‌بندی")]
    [Required(ErrorMessage = "لطفاً نام دسته‌بندی را وارد کنید.")]
    [StringLength(100, ErrorMessage = "نام دسته‌بندی نمی‌تواند بیشتر از 100 کاراکتر باشد.")]
    public string Name { get; set; }

    [Display(Name = "ترتیب نمایش")]
    [Required(ErrorMessage = "لطفاً ترتیب نمایش را وارد کنید.")]
    [Range(1, int.MaxValue, ErrorMessage = "ترتیب نمایش باید یک عدد مثبت باشد.")]
    public int DisplayOrder { get; set; }

    // فایل آپلودی (نوع IFormFile)
    [Display(Name = "آیکون")]
    [Required(ErrorMessage = "لطفاً یک آیکون انتخاب کنید.")]
    public IFormFile IconFile { get; set; }

    // مسیر آیکون موجود (نوع string)
    [Display(Name = "مسیر آیکون موجود")]
    public string ExistingIconPath { get; set; }
}