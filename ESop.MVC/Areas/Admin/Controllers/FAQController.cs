using EShop.Application.Interfaces;
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
                // دریافت لیست دسته‌بندی‌ها از سرویس
                Categories = await _faqCategoryService.GetFAQCategoriesAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(FAQCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _faqService.CreateAsync(model);

            return View();
        }

        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _faqService.GetForUpdateAsync(id);
            if (model == null) return NotFound();
            model.Categories = await _faqCategoryService.GetFAQCategoriesAsync();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(FAQUpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var result = await _faqService.UpdateAsync(model);
            if (result == OperationResult.Success) return RedirectToAction("List");
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
