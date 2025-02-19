using EShop.Application.Interfaces;
using EShop.Domain.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;
using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Colors.Product_Color;
using EShop.Domain.Enums.ColorEnums;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class ProductController : AdminBaseController
    {

        #region Constructor 
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IColorService _colorService;
        public ProductController(IProductService productService,
                                 ICategoryService categoryService,
                                 IColorService colorService)

        {
            _productService = productService;
            _categoryService = categoryService;
            _colorService = colorService;
        }
        #endregion

        #region Product
        #region Get List Of Products Action
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsWithColorsAndPrices();
            return View(products);
        }
        #endregion

        #region Create Product Action
        [HttpGet]
        public async Task<IActionResult> CreateProduct()
        {
            TempData["Categories"] = await _categoryService.GetAllCategoriesInForm();
            //TempData["Colors"] = await _colorService.GetAllColorsList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductViewModel createProduct)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(createProduct);

                switch (result)
                {
                    case CreateProductResult.NotImage:
                        TempData[WarningMessage] = "لطفا یک تصویر انتخاب کنید!";
                        break;

                    case CreateProductResult.Success:
                        TempData[SuccessMessage] = "ثبت محصول با موفقیت انجام شد.";
                        return RedirectToAction("Index");
                }
            }
            return View(createProduct);
        }
        #endregion

        #region Edit Product Action 
        [HttpGet]
        public async Task<IActionResult> EditProduct(int productId)
        {
            var product = await _productService.GetProductForEdit(productId);
            if (product == null)
            {
                return NotFound();
            }

            var categories = await _categoryService.GetAllCategoriesInForm();
            ViewData["Categories"] = categories;
            return View(product);

        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(EditProductViewModel editProduct)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.EditProduct(editProduct);
                switch (result)
                {
                    case EditProductResult.NotFound:
                        TempData[WarningMessage] = "محصول مورد نظر یافت نشد.";
                        break;
                    case EditProductResult.NotProductSelectedCategory:
                        TempData[WarningMessage] = "لطفا دسته بندی محصول را وارد کنید!";
                        break;
                    case EditProductResult.Success:
                        TempData[SuccessMessage] = "ویرایش محصول با موفقیت انجام شد.";
                        return RedirectToAction("Index");

                }
            }
            TempData["Categories"] = await _categoryService.GetAllCategoriesInForm();
            return View(editProduct);
        }
        #endregion

        #region Delete Product Action
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProduct(productId);
            if (result)
            {
                TempData[SuccessMessage] = "حذف محصول با موفقیت انجام شد.";
                return RedirectToAction("Index");
            }

            TempData[WarningMessage] = "محصول مورد نظر یافت نشد.";

            return RedirectToAction("Index");
        }
        #endregion


        #region Add Product Color Mapping Action

        [HttpGet]
        public async Task<IActionResult> AddProductColorMapping(int productId)
        {
            var colors = await _colorService.GetAllColorsList();
            ViewBag.Colors = colors;
            var product = await _productService.GetProductByProductId(productId);
            //var colors = await _colorService.GetAllColors();
            //ViewData["colors"] = colors;
            var viewModel = new AddProductColorViewModel
            {
                ProductId = productId,
                Price = product.Price
                
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductColorMapping(AddProductColorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.AddColorToProduct(model);
                switch (result)
                {
                    case AddProductColorResult.NotFound:
                        TempData[WarningMessage] = "محصول مورد نظر یافت نشد!";
                        break;
                    case AddProductColorResult.Success:
                        TempData[SuccessMessage] = "مپینگ با موفقیت ویرایش شد.";
                        return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        #endregion

        #region Edit Product Color Mapping Action
        [HttpGet]
        public async Task<IActionResult> EditProductColorMapping(int Id)
        {
            var colors = await _colorService.GetAllColors();
            ViewData["colors"] = colors;
            var mapping = await _colorService.GetProductColorMapping(Id);
            var viewModel = new EditProductColorMappingViewModel
            {
                MappingId = Id,

            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditProductColorMapping(EditProductColorMappingViewModel mappingViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _colorService.EditProductColorMapping(mappingViewModel);
                switch (result)
                {
                    case EditProductColorMappingResult.NotFound:
                        TempData[WarningMessage] = "مپینگ مورد نظر یافت نشد!";
                        break;
                    case EditProductColorMappingResult.Success:
                        TempData[SuccessMessage] = "مپینگ با موفقیت ویرایش شد.";
                        return RedirectToAction("Index");
                }
            }
            return View(mappingViewModel);
        }

        #endregion

        #region Delete Mapping Acion
        public async Task<IActionResult> DeleteMappingColor(int Id)
        {
            var result = await _colorService.DeletProductColorMapping(Id);
            if (result)
            {
                TempData[SuccessMessage] = "حذف با موفقیت انجام شد.";
                return RedirectToAction("Index");
            }
            TempData[ErrorMessage] = "حذف انجام نشد!";
            return RedirectToAction("Index");
        }

        #endregion



        #endregion
    }
}
