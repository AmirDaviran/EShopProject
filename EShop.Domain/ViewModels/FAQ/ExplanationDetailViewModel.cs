using System.ComponentModel.DataAnnotations;
namespace EShop.Domain.ViewModels.FAQ;
public class ExplanationDetailViewModel
{
    public int Id { get; set; }

    [Display(Name = "توضیحات")]
    [MaxLength(1000, ErrorMessage = "{0} نمی‌تواند بیش از {1} کاراکتر باشد")]
    public string? Explanation { get; set; }
}