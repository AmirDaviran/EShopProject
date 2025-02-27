using EShop.Domain.ViewModels.Common;

namespace EShop.Domain.ViewModels.Products.Specification;

public class FilterSpecificationViewModel:BasePaging<SpecificationListViewModel>
{
    
    public string? SearchTerm { get; set; } // فیلد جستجو

    public int TakeEntity { get; set; } = 10; // تعداد نمایش داده‌ها
}