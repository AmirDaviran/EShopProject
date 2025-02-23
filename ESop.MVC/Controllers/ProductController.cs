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

   }
}
