using EShop.Application.Interfaces;
using EShop.Application.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Users.Components
{
    public class UserInformationBarViewComponent : ViewComponent
    {
        #region Constructor
        private readonly IUserService _userService;
        public UserInformationBarViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.GetUserById(User.GetUserID());
                return View("UserInformationBar", user);
            }

            return View("UserInformationBar");
        }
    }
}
