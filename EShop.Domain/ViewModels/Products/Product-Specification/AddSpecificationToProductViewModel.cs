using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.Products.Product_Specification;

public class AddSpecificationToProductViewModel
{
    public int ProductId { get; set; }

    [Required(ErrorMessage = "لطفاً یک مشخصه انتخاب کنید")]
    public int SpecificationId { get; set; }

    [Required(ErrorMessage = "لطفاً مقدار را وارد کنید")]
    public string Value { get; set; }
}