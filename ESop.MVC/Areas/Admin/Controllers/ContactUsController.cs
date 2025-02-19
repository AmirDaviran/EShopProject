using EShop.Application.Interfaces;
using EShop.Domain.Enums.ContactUsEnums;
using EShop.Domain.ViewModels.ContactUsViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class ContactUsController : AdminBaseController
    {
        #region Field

        private readonly IContactUsService _contactUsService;

        #endregion

        #region Constructor

        public ContactUsController(IContactUsService contactUsService)
        {
            _contactUsService = contactUsService;
        }

        #endregion

        #region Actions

        #region List

        public async Task<IActionResult> List(FilterContactUsViewModel filter)
        {
            var model = await _contactUsService.FilterAsync(filter);
            return View(model);

        }
        #endregion

        #region Details

        public async Task<IActionResult> Details(int id)
        {
            var contact = await _contactUsService.GetDetailsByIdAsync(id);

            if (contact == null)
            {
                TempData["ErrorMessage"] = "پیام یافت نشد";
                return RedirectToAction(nameof(List));
            }

            return View(contact);
        }

        #endregion

        #region Reply
        [HttpPost]
        public async Task<IActionResult> Reply(ContactUsAdminResponseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

           

            var result = await _contactUsService.AnswerAsync(new ContactUsDetailsViewModel
            {
                Id = model.Id,
                AdminAnswer = model.AdminAnswer
            });

            if (result == AnswerResult.Success)
            {
                TempData["SuccessMessage"] = "پاسخ شما با موفقیت ارسال شد.";
                return RedirectToAction("Details", new { id = model.Id });
            }

            TempData["ErrorMessage"] = "خطایی در ارسال پاسخ رخ داد.";
            return RedirectToAction("Details", new { id = model.Id });
        }

     

        #endregion

        #endregion
    }
}
