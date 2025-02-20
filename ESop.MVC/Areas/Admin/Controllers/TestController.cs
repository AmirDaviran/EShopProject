using EShop.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class TestController : AdminBaseController
    {
        [HttpGet ("/test")]
        public IActionResult Test()
        {
            return View();
        }
    }
}
