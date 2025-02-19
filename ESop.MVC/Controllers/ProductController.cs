using EShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class ProductController : BaseController
    {
        #region Constructor
        IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        #endregion
        #region Product
        public IActionResult Index()
        {
            return View();
        }


        #region Show Product

        [HttpGet("ShowProduct/{productId}")]
        public async Task<IActionResult> ProductDetail(int productId)
        {
            var product = await _productService.ShowProductDetails(productId);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        #endregion
        #endregion

    }
}
