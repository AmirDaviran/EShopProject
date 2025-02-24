using EShop.Application.Interfaces;
using EShop.Domain.Entities.FAQ;
using EShop.Domain.Enums.FAQEnum;
using EShop.Domain.ViewModels.FAQ;
using Microsoft.AspNetCore.Mvc;
namespace EShop.Web.Areas.Admin.Controllers
{
    public class FAQController(IFAQService _faqService, IFAQCategoryService _faqCategoryService) : AdminBaseController
    {
        #region List
        public async Task<IActionResult> List()
        {
            var faqs = await _faqService.GetAllFAQForAdminAsync();
            return View(faqs);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new FAQCreateViewModel
            {
                Categories = await _faqCategoryService.GetFAQCategoriesAsync()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FAQCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _faqCategoryService.GetFAQCategoriesAsync();
                TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(model);
            }

            var result = await _faqService.CreateAsync(model);
            switch (result)
            {
                case OperationResult.Success:
                    TempData[SuccessMessage] = "سوال با موفقیت ایجاد شد.";
                    return RedirectToAction("List");
                case OperationResult.Failure:
                    TempData[ErrorMessage] = "خطایی در ایجاد سوال رخ داد.";
                    break;
                case OperationResult.NotFound:
                    TempData[ErrorMessage] = "دسته‌بندی انتخاب‌شده یافت نشد.";
                    break;
            }

            return View(model);
        }

        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _faqService.GetForUpdateAsync(id);
            if (model == null)
            {
                TempData[ErrorMessage] = "سوال موردنظر یافت نشد.";
                return NotFound();
            }
            model.Categories = await _faqCategoryService.GetFAQCategoriesAsync() ?? new List<FAQCategory>();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FAQUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = await _faqCategoryService.GetFAQCategoriesAsync() ?? new List<FAQCategory>();
                TempData[ErrorMessage] = "لطفاً اطلاعات را به درستی وارد کنید.";
                return View(model);
            }

            var result = await _faqService.UpdateAsync(model);
            switch (result)
            {
                case OperationResult.Success:
                    TempData[SuccessMessage] = "سوال با موفقیت به‌روزرسانی شد.";
                    return RedirectToAction("List");
                case OperationResult.Failure:
                    TempData[ErrorMessage] = "خطایی در به‌روزرسانی سوال رخ داد.";
                   break;
                case OperationResult.NotFound:
                    TempData[ErrorMessage] = "سوال موردنظر یافت نشد.";
                    break;
               
            }

            return View(model);
        }
        #endregion

        #region Details (Explanation)
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await _faqService.GetExplanationAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        #endregion

        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _faqService.DeleteAsync(id);
            if (result == OperationResult.Success) return RedirectToAction("List");
            return NotFound();
        }
        #endregion
    }
}
