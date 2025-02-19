using Microsoft.AspNetCore.Mvc;
using EShop.Application.Interfaces;
using EShop.Domain.ViewModels.FAQ;
using EShop.Domain.Enums.FAQEnum;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class FAQController : AdminBaseController
    {
        #region Fields

        private readonly IFAQService _faqService;

        #endregion

        #region Constructor

        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }

        #endregion

        #region List

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var model = await _faqService.GetAllAsync();
            return View(model);
        }
        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFAQViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _faqService.CreateAsync(model);
            switch (result)
            {
                case OperationResult.Success:
                    TempData[SuccessMessage] = "ایجاد با موفقیت انجام شد.";
                    return RedirectToAction(nameof(List));

                case OperationResult.Failure:
                    TempData[ErrorMessage] = "خطا در ایجاد سوال متداول.";
                    break;

                default:
                    TempData[WarningMessage] = "خطایی رخ داده است.";
                    break;
            }
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var faq = await _faqService.GetForUpdateAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateFAQViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _faqService.UpdateAsync(model);
            if (result == OperationResult.Success)
            {
                TempData[SuccessMessage] = "بروزرسانی با موفقیت انجام شد.";
                return RedirectToAction(nameof(List));
            }
            else if (result == OperationResult.NotFound)
            {
                TempData[WarningMessage] = "آیتم موردنظر یافت نشد.";
                return NotFound();
            }
            else
            {
                TempData[ErrorMessage] = "خطا در بروزرسانی سوال متداول.";
                return View(model);
            }
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _faqService.DeleteAsync(id);
            if (result == OperationResult.Success)
            {
                TempData[SuccessMessage] = "حذف با موفقیت انجام شد.";
            }
            else if (result == OperationResult.NotFound)
            {
                TempData[WarningMessage] = "آیتم موردنظر یافت نشد.";
            }
            else
            {
                TempData[ErrorMessage] = "خطا در حذف سوال متداول.";
            }
            return RedirectToAction(nameof(List));
        }
        #endregion
    }
}
