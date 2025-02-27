namespace EShop.Domain.ViewModels.Products.Product_Specification;

public class ProductSpecificationListViewModel
{
    public int MappingId { get; set; } // کامنت: برای شناسایی هر ردیف در لیست
    public int SpecificationId { get; set; }
    public string SpecificationName { get; set; } // کامنت: برای نمایش نام مشخصه
    public string Value { get; set; }
}