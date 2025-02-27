using EShop.Application.Interfaces;
using EShop.Application.Services;
using EShop.Domain.Enums.ProductEnums;
using EShop.Domain.ViewModels.Products.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class ProductController(IProductService _productService,ICategoryService _categoryService) : AdminBaseController
    {

        #region List
        public async Task<IActionResult> List( FilterProductViewModel model)
        {
            var result = await _productService.FilterAsync(model);
            return View(result);
        }

        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Title");
            return View(new CreateProductViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            #region Validation

            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(model);
            }

            #endregion

            var result = await _productService.CreateAsync(model);

            switch (result)
            {
                case CreateProductResult.Success:
                    TempData[SuccessMessage] = "محصول با موفقیت ایجاد شد";
                    return RedirectToAction(nameof(List));
                case CreateProductResult.InvalidInput:
                    TempData[ErrorMessage] = "ورودی نامعتبر است";
                    break;
                case CreateProductResult.ImageUploadFailed:
                    TempData[ErrorMessage] = "عکس آپلود نشد";
                    break;
            }
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Title");
            return View(model);
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _productService.GetForUpdateAsync(id);
            if (model == null)
            {
                TempData[ErrorMessage] = "محصول مورد نظر یافت نشد";
                return RedirectToAction(nameof(List));
            }
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Title");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductViewModel model)
        {
            #region Validation
            if(!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(model);
            }
            #endregion

            var result = await _productService.UpdateAsync(model);

            switch (result)
            {
                case UpdateProductResult.Success:
                    TempData[SuccessMessage] = "محصول با موفقیت ویرایش شد";
                    return RedirectToAction(nameof(List));
                    case UpdateProductResult.NotFound:
                    TempData[ErrorMessage]= "محصول مورد نظر یافت نشد";
                    break;
                case UpdateProductResult.InvalidInput:
                    TempData[ErrorMessage] = "ورودی نامعتبر است";
                    break;
                case UpdateProductResult.ImageUploadFailed:
                    TempData[ErrorMessage] = "عکس آپلود نشد";
                    break;
                }
            ViewBag.Categories = new SelectList(await _categoryService.GetAllCategoriesAsync(), "Id", "Title");
            return View(model);
        }

        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
           var result= await _productService.DeleteAsync(id);

         
           if (result == DeleteProductResult.Success)
           {
               TempData[SuccessMessage] = "محصول با موفقیت حذف شد";
           }
           else if (result == DeleteProductResult.NotFound)
           {
               TempData[ErrorMessage] = "محصول مورد نظر یافت نشد";
           }

           return RedirectToAction(nameof(List));
        }

        #endregion

    }

}

