using EShop.Application.Interfaces;
using EShop.Domain.Entities.FAQ;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FAQCategoryController : AdminBaseController
    {
        private readonly IFAQCategoryService _faqCategoryService;

        public FAQCategoryController(IFAQCategoryService faqCategoryService)
        {
            _faqCategoryService = faqCategoryService;
        }

        public async Task<IActionResult> List()
        {
            var categories = await _faqCategoryService.GetFAQCategoriesAsync();
            return View(categories);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("CreateOrEdit", new FAQCategory());
        }

        [HttpPost]
        public async Task<IActionResult> Create(FAQCategory model, IFormFile iconFile)
        {
            // آپلود آیکون
            if (iconFile != null)
            {
                var uploadPath = Path.Combine("wwwroot", "uploads", "faq-icons");
                var fileName = Guid.NewGuid() + Path.GetExtension(iconFile.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                Directory.CreateDirectory(uploadPath);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await iconFile.CopyToAsync(stream);
                }

                model.Icon = $"/uploads/faq-icons/{fileName}";
            }

            await _faqCategoryService.AddCategoryAsync(model);
            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _faqCategoryService.GetFAQCategoryByIdAsync(id);
            return View("CreateOrEdit", category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(FAQCategory model, IFormFile iconFile)
        {
            // آپلود آیکون جدید (اگر وجود داشت)
            if (iconFile != null)
            {
                // حذف آیکون قدیمی (اختیاری)
                if (!string.IsNullOrEmpty(model.Icon))
                {
                    var oldPath = Path.Combine("wwwroot", model.Icon.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                // آپلود جدید
                var uploadPath = Path.Combine("wwwroot", "img", "faq-icons");
                var fileName = Guid.NewGuid() + Path.GetExtension(iconFile.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                Directory.CreateDirectory(uploadPath);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await iconFile.CopyToAsync(stream);
                }

                model.Icon = $"/uploads/faq-icons/{fileName}";
            }

            await _faqCategoryService.UpdateCategoryAsync(model);
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _faqCategoryService.DeleteCategoryAsync(id);
            return RedirectToAction("List");
        }
    }
}