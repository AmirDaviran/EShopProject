using EShop.Application.Interfaces;
using EShop.Domain.Enums.ContactUsEnums;
using EShop.Domain.ViewModels.ContactUsViewModel;
using EShop.Web.Areas.Admin.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Controllers
{
    public class ContactUsController : BaseController
    {
        #region Fields

        private readonly IContactUsService _contactUsService;

        #endregion

        #region Constructor

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        #endregion

        #region Actions

        #region Create

        [HttpGet("/contact-us")]
        public async Task<IActionResult> ContactUs()
        {
            return View();
        }

        [HttpPost("/contact-us")]
        public async Task<IActionResult> ContactUs(CreateContactUsViewModel model)
        {
            #region Validations

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #endregion

            var result= await _contactUsService.CreateAsync(model);

            if (result == CreateContactUsResult.Success)
            {
                TempData["SuccessMessage"] = "پیام شما با موفقیت ارسال شد";
                return RedirectToAction(nameof(ContactUs));
            }
            else
            {
                TempData["ErrorMessage"] = "خطا در ارسال پیام";
                return View(model);
            }

        }

        #endregion

        #endregion



    }
}
