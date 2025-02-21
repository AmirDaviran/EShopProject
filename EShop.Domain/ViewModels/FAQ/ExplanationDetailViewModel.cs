using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.FAQ;

public class ExplanationDetailViewModel
{
    int Id { get; set; }

    [Display(Name = "توضیحات")]
    public string? Explanation { get; set; }
}