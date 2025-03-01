﻿using EShop.Application.Interfaces;
using EShop.Domain.Enums.FAQEnum;
using EShop.Domain.ViewModels.FAQCategory;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{

    public class FAQCategoryController(IFAQCategoryService _faqCategoryService) : AdminBaseController
    {

        #region List

        public async Task<IActionResult> List()
        {
            var categories = await _faqCategoryService.GetCategoriesOrderedAsync();
            return View(categories);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View(new FAQCategoryCreateViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FAQCategoryCreateViewModel viewModel)
        {
            #region Vlidation
            ModelState.Remove("ExistingIconPath"); // حذف اعتبارسنجی برای این فیلد
            if (!ModelState.IsValid)
            {
                TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(viewModel);
            }
            #endregion

           
            var result = await _faqCategoryService.CreateFAQCategoryAsync(viewModel);

            switch (result)
            {
                case CreateFAQCategoryResult.Success:
                    TempData[SuccessMessage] = "دسته‌بندی با موفقیت ایجاد شد.";
                    return RedirectToAction("List");

                case CreateFAQCategoryResult.Failure:
                    TempData["ErrorMessage"] = "خطایی در ایجاد دسته‌بندی رخ داده است.";
                    break;
            }

            return View(viewModel);
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _faqCategoryService.GetFAQCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            // نگاشت داده‌های Domain به ViewModel مخصوص ویرایش
            var viewModel = new FAQCategoryUpdateViewModel
            {
                Id = category.Id,
                Name = category.Name,
                DisplayOrder = category.DisplayOrder,
                ExistingIconPath = category.Icon
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FAQCategoryUpdateViewModel viewModel)
        {
            //ModelState.Remove("ExistingIconPath"); // حذف اعتبارسنجی برای این فیلد
            //if (!ModelState.IsValid)
            //{
            //    TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
            //    return View(viewModel);
            //}



            var result = await _faqCategoryService.UpdateFAQCategoryAsync(viewModel);
            switch (result)
            {
                case UpdateFAQCategoryResult.Success:
                    TempData["SuccessMessage"] = "دسته‌بندی با موفقیت به‌روزرسانی شد.";
                    return RedirectToAction("List");
                case UpdateFAQCategoryResult.NotFound:
                    TempData["ErrorMessage"] = "دسته‌بندی مورد نظر یافت نشد.";
                    return NotFound();
                    break;
                case UpdateFAQCategoryResult.Failure:
                    TempData["ErrorMessage"] = "خطایی در به‌روزرسانی دسته‌بندی رخ داده است.";
                    break;
            }

            return View(viewModel);

        }

        #endregion

        #region Delete

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _faqCategoryService.DeleteCategoryAsync(id);
            return RedirectToAction("List");
        }

        #endregion
    }
}
