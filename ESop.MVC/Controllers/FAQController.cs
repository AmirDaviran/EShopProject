using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class FAQController : BaseController
    {
        public async Task<IActionResult> List()
        {
            return View();
        }
    }
}
