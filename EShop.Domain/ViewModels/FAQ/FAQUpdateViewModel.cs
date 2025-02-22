using EShop.Domain.Entities.FAQ;
using System.ComponentModel.DataAnnotations;
namespace EShop.Domain.ViewModels.FAQ;
public class FAQUpdateViewModel
{
    public int Id { get; set; }

    [Display(Name = "سوال")]
    [MaxLength(250, ErrorMessage = "{0} نمی‌تواند بیش از {1} کاراکتر باشد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Question { get; set; }

    [Display(Name = "جواب")]
    [MaxLength(800, ErrorMessage = "{0} نمی‌تواند بیش از {1} کاراکتر باشد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Answer { get; set; }

    [Display(Name = "توضیحات")]
    [MaxLength(1000, ErrorMessage = "{0} نمی‌تواند بیش از {1} کاراکتر باشد")]
    public string? Explanation { get; set; }

    [Display(Name = "دسته بندی")]
    [Required(ErrorMessage = "لطفا دسته بندی را انتخاب کنید")]
    public int CategoryId { get; set; }

    public List<Entities.FAQ.FAQCategory>? Categories { get; set; }
}