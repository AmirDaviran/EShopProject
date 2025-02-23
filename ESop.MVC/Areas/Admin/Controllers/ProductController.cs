using EShop.Application.Interfaces;
using EShop.Domain.Enums.ColorEnums;
using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.ViewModels.Products;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class ProductController
        (IProductService _productService,
        ICategoryService _categoryService
         )
        : AdminBaseController
    {

        #region Product
        #region Get List Of Products Action
        public async Task<IActionResult> Index()
        {
           
            return View();
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

             

        #endregion
    }
}
