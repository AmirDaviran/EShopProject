using EShop.Application.Interfaces;
using EShop.Domain.ViewModels.FAQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class FAQController(IFAQService _faqService) : AdminBaseController
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
            return View();
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
    }
}
