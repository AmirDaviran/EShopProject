namespace EShop.Domain.Enums.ProductSpecificationMapping;


public enum ProductSpecificationMappingResult
{
    Success,             // عملیات با موفقیت انجام شد
    InvalidInput,        // ورودی نامعتبر است
    NotFound,           // مورد موردنظر یافت نشد
    Failed              // عملیات با شکست مواجه شد
}