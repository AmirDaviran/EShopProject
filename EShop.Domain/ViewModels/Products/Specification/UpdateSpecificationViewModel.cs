using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.Products.Specification;

public class UpdateSpecificationViewModel
{
    public int Id { get; set; }

    [Display(Name = "نام مشخصه")]
    [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
    [MaxLength(250, ErrorMessage = "{0} نمی‌تواند بیشتر از {1} کاراکتر باشد")]
    public string Name { get; set; }

}