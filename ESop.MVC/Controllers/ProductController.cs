using EShop.Application.Interfaces;
using EShop.Domain.ViewModels.Products.Product;
using EShop.Domain.ViewModels.Products.Product.ClientSide;
using EShop.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

public class ProductController : BaseController
{
    #region Fields
    private readonly IProductService _productService;
    #endregion

    #region Constructor
    public ProductController(IProductService myProductService)
    {
        _productService = myProductService;
    }
    #endregion


    #region Actions

    #region List
    //[HttpGet("/product-list")]
    public async Task<IActionResult> List(FilterClientSideProductViewModel filter)
    {
        var result = await _productService.FilterClientAsync(filter);
        return View(filter);
    }
    #endregion

    #region 
    [HttpGet("/product-details")]
    public async Task<IActionResult> Details(int id)
    {
        var mySections = await _productService.GetMyProductSectionsAsync(id);
        if (mySections == null) return NotFound();

        var viewModel = new { MySections = mySections };
        return View(viewModel);
    }
    #endregion

    
    #endregion
}