using EShop.Application.Interfaces;
using EShop.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

public class ProductController : BaseController
{
    private readonly IProductService _productService;

    public ProductController(IProductService myProductService)
    {
        _productService = myProductService;
    }

    public async Task<IActionResult> Details(int id)
    {
        var mySections = await _productService.GetMyProductSectionsAsync(id);
        if (mySections == null) return NotFound();

        var viewModel = new { MySections = mySections };
        return View(viewModel);
    }
}