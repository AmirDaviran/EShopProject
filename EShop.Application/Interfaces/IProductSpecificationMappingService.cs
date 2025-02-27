using EShop.Domain.Enums.ProductSpecificationMapping;
using EShop.Domain.ViewModels.Products.Product_Specification;

namespace EShop.Application.Interfaces
{
    public interface IProductSpecificationMappingService
    {
        Task<bool> AddSpecificationToProductAsync(AddSpecificationToProductViewModel model); // کامنت: تغییر نوع ورودی
        Task<List<ProductSpecificationListViewModel>> GetSpecificationsByProductIdAsync(int productId); // کامنت: تغییر نوع بازگشت
        Task<bool> RemoveSpecificationFromProductAsync(int mappingId);

        Task<bool> UpdateSpecificationAsync(int mappingId, AddSpecificationToProductViewModel model);
    }

   
}