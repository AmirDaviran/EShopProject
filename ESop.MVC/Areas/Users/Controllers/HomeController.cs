using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Users.Controllers
{
    public class HomeController : UserBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
