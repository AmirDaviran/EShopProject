using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.ViewModels.Products.ClientSide;
using EShop.Domain.ViewModels.Products.Product;
using EShop.Domain.ViewModels.Products.Product.ClientSide;
namespace EShop.Application.Interfaces
{
    public interface IProductService
    {
        #region Admin Side

        Task<List<ProductListViewModel>> GetAllAsync();
        Task<UpdateProductViewModel> GetForUpdateAsync(int id);
        Task<UpdateProductResult> UpdateAsync(UpdateProductViewModel model);
        Task<CreateProductResult> CreateAsync(CreateProductViewModel model);
        Task<DeleteProductResult> DeleteAsync(int id);
        Task<FilterProductViewModel> FilterAsync(FilterProductViewModel model);
        //Task<List<ProductListViewModel>> GetProductsByCategoryIdAsync(int categoryId);

        #endregion

        #region Client Side

        Task<MyProductSectionsViewModel> GetMyProductSectionsAsync(int productId);
        Task<FilterClientSideProductViewModel> FilterClientAsync(FilterClientSideProductViewModel model);
        #endregion
    }
}
