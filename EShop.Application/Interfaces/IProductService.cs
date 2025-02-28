using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.ViewModels.Products.ClientSide;
using EShop.Domain.ViewModels.Products.Product;
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
        Task<List<ProductListViewModel>> GetProductsByCategoryIdAsync(int categoryId);

        #endregion

        #region Client Side

        Task<ProductDetailViewModel> GetProductDetailAsync(int productId);
        Task<List<RelatedProductViewModel>> GetRelatedProductsAsync(int categoryId);

        #endregion
    }
}
