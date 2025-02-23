using EShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class ProductController (IProductService _productService) : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        #region Show Product

        [HttpGet("ShowProduct/{productId}")]
        public async Task<IActionResult> ProductDetail(int productId)
        {
            var product = await _productService.ShowProductDetails(productId);
            if (product is null)
                return NotFound();
            
            return View(product);
        }


        #endregion
    }
}
