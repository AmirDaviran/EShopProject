using System.ComponentModel.DataAnnotations;
namespace EShop.Domain.ViewModels.FAQ;
public class FAQViewModel
{
    public int Id { get; set; }
    public DateTime CreatedDate { get; set; }

    [Display(Name = "سوال")]
    [MaxLength(250, ErrorMessage = "{0} نمی‌تواند بیش از {1} کاراکتر باشد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Question { get; set; }

    [Display(Name = "جواب")]
    [MaxLength(800, ErrorMessage = "{0} نمی‌تواند بیش از {1} کاراکتر باشد")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    public string Answer { get; set; }

    public string CategoryName { get; set; }
    public int CategoryId { get; set; }
}