using System.ComponentModel.DataAnnotations;
using EShop.Domain.ViewModels.Common;

namespace EShop.Domain.ViewModels.Products.Product;

public class FilterProductViewModel:BasePaging<ProductListViewModel>
{
    public string? SearchTerm { get; set; } // فیلد جستجو

    public int TakeEntity { get; set; } = 10; // تعداد نمایش داده‌ها
}