using EShop.Application.Interfaces;
using EShop.Application.Utilities.Extensions;
using EShop.Domain.Entities.Account;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Users.Components
{
    public class UserSideBarViewComponent : ViewComponent
    {
        #region Constructor
        private readonly IUserService _userService;
        public UserSideBarViewComponent(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.IsAuthenticated)
            {

                var user = await _userService.GetUserById(User.GetUserID());
                return View("UserSideBar", user);
            }
            return View("UserSideBar");
        }
    }
}
