using EShop.Application.Interfaces;
using EShop.Domain.Enums.TicketEnums;
using EShop.Domain.ViewModels.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class TicketController (ITicketService _ticketService): AdminBaseController
    {

        #region Get Ticket Lists Action
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsInAdmin();
            return View(tickets);
        }

        #endregion

        #region Ticket Details Action
        [HttpGet]
        [Route("/Admin/Ticket/Details/{ticketId}")]
        public async Task<IActionResult> Details(int ticketId)
        {
            var ticket = await _ticketService.GetTicketByTicketID(ticketId);
            var showTicket = new TicketDetailsViewModel()
            {
                Ticket = ticket,
                Conversations = await _ticketService.GetTicketConversationsByTicketId(ticketId),
                UpdatedDate = ticket.UpdatedDate,
            };

            return View(showTicket);
        }

        #endregion

        #region Update Ticket Conversation Action


        [HttpPost]
        public async Task<IActionResult> UpdateTicketConversationInAdmin(UpdateTicketMessagesViewModel model, int ticketId)
        {
            await _ticketService.UpdateTicketConversation(model);
            return RedirectToAction("Details", new { ticketId = model.TicketId });
        }

        #endregion

        #region Create Ticket Action
        [HttpGet]
        public IActionResult CreateTicket(int id)
        {
            CreateTicketViewModel model = new CreateTicketViewModel()
            {
                OwnerId = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTicket(CreateTicketViewModel model)
        {
            if (ModelState.IsValid)
            {

                var result = await _ticketService.CreateTicket(model);
                switch (result)
                {
                    case CreateTicketResult.IsNotImageOrPDF:
                        TempData[WarningMessage] = "نوع فایل انتخابی نادرست است!";
                        break;

                    case CreateTicketResult.Failure:
                        TempData[ErrorMessage] = "فایل ذخیره نشد!";
                        break;
                    case CreateTicketResult.Success:
                        TempData[SuccessMessage] = "تیکت با موفقیت ارسال شد.";

                        return RedirectToAction("Index");


                }
            }
            return View();
        }

        #endregion

        #region Delete a Ticket Action

      
        public async Task<IActionResult> DeleteTicket(int ticketId)
        {
            var result = await _ticketService.DeleteTicket(ticketId);

            if (result)
            {
                TempData[SuccessMessage] = "حذف با موفقیت انجام شد.";
                return RedirectToAction("Index");
            }

            TempData[ErrorMessage] = "حذف انجام نشد!";

            return RedirectToAction("Index");
        }

        #endregion

        #region Update Ticket Status Action
        // [HttpPost]
        // public async Task<IActionResult> UpdateTicketStatus(int ticketId, TicketStatus status)
        // {
        //    var result = await _ticketService.UpdateTicketStatus(ticketId, status);
        //     if (result)
        //     {
        //         TempData[SuccessMessage] = "وضعیت تغییر یافت";
        //         return RedirectToAction("Index");
        //     }
        //     TempData[ErrorMessage] = "تغییر انجام نشد!";
        //     return View("Details",ticketId);
        //
        // }
        #endregion

    }
}
