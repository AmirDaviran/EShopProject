using EShop.Domain.ViewModels.Common;

namespace EShop.Domain.ViewModels.Products.Product_Specification;

public class ProductSpecificationFilterViewModel:BasePaging<ProductSpecificationListViewModel>
{
    public int ProductId { get; set; } 
    public string Vlaue { get; set; } //  برای نمایش نام مشخصه

    public string? SearchTerm { get; set; } // فیلد جستجو

    public int TakeEntity { get; set; } = 10; // تعداد نمایش داده‌ها
}