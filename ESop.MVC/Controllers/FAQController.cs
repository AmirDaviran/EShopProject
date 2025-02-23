using EShop.Application.Interfaces;
using EShop.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class FAQController(IFAQService _faqService, IFAQCategoryService _faqCategoryService) : BaseController
    {
        // نمایش صفحه اصلی FAQ
        public async Task<IActionResult> Index()
        {
            var categories = await _faqCategoryService.GetFAQCategoriesAsync();
            return View(categories);
        }

        // نمایش FAQ‌های یک دسته‌بندی خاص
        public async Task<IActionResult> Category(int id)
        {
            var category = await _faqCategoryService.GetFAQCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            var faqs = await _faqService.GetFAQsByCategoryIdAsync(id);
            ViewBag.Category = category;
            return View(faqs);
        }

        // نمایش جزئیات یک FAQ خاص
        public async Task<IActionResult> Question(int id)
        {
            var faq = await _faqService.GetFAQByIdAsync(id);
            if (faq == null)
            {
                return NotFound();
            }
            return View(faq);
        }
    }
}
