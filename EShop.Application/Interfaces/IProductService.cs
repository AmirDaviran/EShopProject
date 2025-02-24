using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.ViewModels.Products.Product;
namespace EShop.Application.Interfaces
{
    public interface IProductService
    {
        Task<List<ProductViewModel>> GetAllAsync();
        Task<UpdateProductViewModel> GetForUpdateAsync(int id);
        Task<UpdateProductResult> UpdateAsync(UpdateProductViewModel model);
        Task<CreateProductResult> CreateAsync(CreateProductViewModel model);
        Task<DeleteProductResult> DeleteAsync(int id);

    }
}
