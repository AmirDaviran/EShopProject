using EShop.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class FAQController : BaseController
    {
        private readonly IFAQService _faqService;

        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }

      
        public async Task<IActionResult> Index()
        {
            var faqs = await _faqService.GetAllAsync();
            return View(faqs);
        }
    }
}
