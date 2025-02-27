using EShop.Application.Interfaces;
using EShop.Domain.Enums.CategoryEnums;
using EShop.Domain.ViewModels.Products.Category;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class CategoryController(ICategoryService _categoryService) : AdminBaseController
    {
        #region List

        public async Task<IActionResult> List()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return View(categories);
        }
        #endregion

        #region Create

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            ViewBag.ParentCategories = new SelectList(categories, "Id", "Title");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            #region validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion
            
            var result = await _categoryService.CreateCategoryAsync(model);
            switch (result)
            {
                case CreateCategoryResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد.";
                    return RedirectToAction(nameof(List));

                case CreateCategoryResult.Error:
                    TempData[ErrorMessage] = "خطا در انجام عملیات.";
                    break;

                case CreateCategoryResult.DuplicateTitle:
                    TempData[ErrorMessage] = "عنوان تکراری است.";
                    break;
            }
            
            return View();
        }

        #endregion

        #region Update

        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);

            if (category is null)
                return NotFound();
            
            var model = new EditCategoryViewModel()
            {
                Id = category.Id,
                Title = category.Title,
                ParentCategoryId = category.ParentCategoryId,
                DisplayOrder = category.DisplayOrder
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditCategoryViewModel model)
        {
            #region validation
            if (!ModelState.IsValid)
                return View(model);
            #endregion

            var result = await _categoryService.EditCategoryAsync(model);

            switch (result)
            {
                case EditCategoryResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد.";
                    return RedirectToAction(nameof(List));
                    break;
                case EditCategoryResult.NotFound:
                    TempData[ErrorMessage]= "دسته بندی یافت نشد.";
                    break;
                case EditCategoryResult.DuplicatedTitle:
                    TempData[ErrorMessage]= "عنوان تکراری است.";
                    break;
                case EditCategoryResult.Failure:
                    TempData[ErrorMessage] = "خطا در انجام عملیات.";
                    break;
                case EditCategoryResult.InvalidParent:
                    TempData[ErrorMessage] = "والد انتخاب ‌شده معتبر نیست.";
                    break;
            }

            return View(model);
        }

        #endregion

        #region Delete

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result= await _categoryService.DeleteCategoryAsync(id);

            switch (result)
            {
                case DeleteCategoryResult.Success:
                    TempData[SuccessMessage] = "عملیات با موفقیت انجام شد.";
                    return RedirectToAction(nameof(List));
                    break;
                case DeleteCategoryResult.NotFound:
                    TempData[ErrorMessage] = "دسته بندی یافت نشد.";
                    break;
                case DeleteCategoryResult.HasSubCategories:
                    TempData[ErrorMessage] = "دسته بندی دارای زیر دسته است.";
                    break;
                case DeleteCategoryResult.Failure:
                    TempData[ErrorMessage] = "خطا در انجام عملیات.";
                    break;
            }

            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
