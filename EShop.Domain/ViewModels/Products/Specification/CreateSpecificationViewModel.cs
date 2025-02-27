using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.Products.Specification;

public class CreateSpecificationViewModel
{
    [Display(Name = "مشخصه ")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(250, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد")]
    public string Name { get; set; }
}