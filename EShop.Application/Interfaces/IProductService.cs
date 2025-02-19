using EShop.Domain.Entities.ProductEntity;
using EShop.Domain.Enums.ColorEnums;
using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.ViewModels.Colors.Product_Color;
using EShop.Domain.ViewModels.Products;
using EShop.Domain.ViewModels.Products.Site_Side;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Application.Interfaces
{
    public interface IProductService
    {
       Task<List<ProductListViewModel>> GetAllProducts();
        Task<Product> GetProductByProductId(int productId);
        Task<CreateProductResult> CreateProduct(CreateProductViewModel createProduct);
        Task<bool> DeleteProduct(int productId);
        Task UpdateProduct(Product product);

        Task<List<ProductSelectedCategory>> GetAllProductCategories();
        Task<EditProductViewModel> GetProductForEdit(int productId);
        Task<EditProductResult> EditProduct(EditProductViewModel editProduct);

        #region Product Color Mapping
        Task<AddProductColorResult> AddColorToProduct(AddProductColorViewModel model);
        Task<List<GetProductColorMappingsViewModel>> GetProductColorMappings(int productId);
        Task<List<ProductWithColorsViewModel>> GetAllProductsWithColorsAndPrices();
        #endregion

        #region Site Side
        Task<ProductDetailsViewModel> ShowProductDetails(int productId);
        #endregion

    }
}
