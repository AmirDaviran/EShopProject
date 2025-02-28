using EShop.Application.Interfaces;
using EShop.Domain.ViewModels.Products.ClientSide;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class ProductController : BaseController
    {
        #region Fields

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IProductSpecificationMappingService _productSpecificationMappingService;

        #endregion

        #region Constructor

        public ProductController(IProductService productService, ICategoryService categoryService, IProductSpecificationMappingService productSpecificationMappingService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _productSpecificationMappingService = productSpecificationMappingService;
        }

        #endregion
        
            [HttpGet]
            public async Task<IActionResult> Details(int id)
            {
                var product = await _productService.GetProductDetailAsync(id);
                if (product == null) return NotFound();

                var relatedProducts = await _productService.GetRelatedProductsAsync(
                    product.ProductId); // فرضاً از دسته‌بندی محصول استفاده می‌کنیم
                product.RelatedProducts = relatedProducts;

                return View(product);
            }
        }
    }

