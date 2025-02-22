using EShop.Application.Interfaces;
using EShop.Domain.Enums.UserEnums;
using EShop.Domain.ViewModels.UserViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class UserController : AdminBaseController
    {
        #region Field

        private readonly IUserService _userService;

        #endregion

        #region Constructor

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #endregion

        #region Actions

        #region List

        public async Task<IActionResult> List(FilterUserViewModel model)
        {
            var result = await _userService.FilterAsync(model);

            return View(result);
        }

        #endregion

        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }


            var result = await _userService.CreateUserAsync(dto);

            switch (result)
            {
                case CreateUserResult.Success:
                    TempData[SuccessMessage] = "کاربر با موفقیت ایجاد شد.";
                    return RedirectToAction("List");

                case CreateUserResult.Error:
                    TempData[ErrorMessage] = "خطایی در ایجاد کاربر رخ داده است.";
                    break;
                    

            }

            return View(dto);
        }

        #endregion

        #region Update

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _userService.GetUserForEditByIdAsync(id);

            if (user == null)
            {
                TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد.";
                return RedirectToAction("List");
            }

            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userService.UpdateUserAsync(model);

            switch (result)
            {
                case EditUserResult.Success:
                    TempData[SuccessMessage] = "کاربر با موفقیت ویرایش شد.";
                    return View(model);

                case EditUserResult.UserNotFound:
                    TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد.";
                    return RedirectToAction("List");

                case EditUserResult.EmailDuplicated:
                    TempData[ErrorMessage] = "ایمیل وارد شده تکراری است.";
                    return View(model);

                case EditUserResult.MobileDuplicated:
                    TempData[ErrorMessage] = "شماره موبایل وارد شده تکراری است.";
                    return View(model);
            }

            return View(model);
        }

        #endregion

        #region #Delete

        [HttpPost]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var result = await _userService.SoftDeleteByAdminAsync(id);

            if (result)
            {
                TempData[SuccessMessage] = "کاربر با موفقیت حذف شد.";
            }
            else
            {
                TempData[ErrorMessage] = "خطایی در حذف کاربر رخ داده است.";
            }

            return RedirectToAction("List");
        }
        #endregion

        #endregion

    }
}
