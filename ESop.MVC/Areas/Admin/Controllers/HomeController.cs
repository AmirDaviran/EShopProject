using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{

    [Area("Admin")]

    public class HomeController : AdminBaseController
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
