using EShop.Application.Interfaces;
using EShop.Domain.Enums.TicketEnums;
using EShop.Domain.ViewModels.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Admin.Controllers
{
    public class TicketController(ITicketService _ticketService) : AdminBaseController
    {
        
        #region GetTicketListsAction
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsInAdminAsync();
            return View(tickets);
        }

        #endregion

        #region TicketDetailsAction
        [HttpGet]
        [Route("/Admin/Ticket/Details/{ticketId}")]
        public async Task<IActionResult> Details(int ticketId)
        {
            var ticket = await _ticketService.GetTicketByTicketIDAsync(ticketId);
            var showTicket = new TicketDetailsViewModel()
            {
                Ticket = ticket,
                Conversations = await _ticketService.GetTicketConversationsByTicketIdAsync(ticketId),
                UpdatedDate = ticket.UpdatedDate,
            };

            return View(showTicket);
        }

        #endregion

        #region UpdateTicketConversation Action

        [HttpPost]
        public async Task<IActionResult> UpdateTicketConversationInAdmin(UpdateTicketMessagesViewModel model, int ticketId)
        {
            await _ticketService.UpdateTicketConversationAsync(model);
            return RedirectToAction("Details", new { ticketId = model.TicketId });
        }

        #endregion

        #region CreateTicket 
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
            if (!ModelState.IsValid) 
                return View(model);

            var result = await _ticketService.CreateTicketAsync(model);
            if (result == CreateTicketResult.Success)
            {
                TempData[SuccessMessage] = "تیکت با موفقیت ارسال شد.";
                return RedirectToAction("Index");
            }

            TempData[ErrorMessage] = "خطایی در ثبت تیکت رخ داد.";
            return View(model);
        }

        #endregion

        #region DeleteTicketAction

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

        #region UpdateTicketStatusAction
        [HttpPost]
        public async Task<IActionResult> UpdateTicketStatus(int ticketId, TicketStatus status)
        {
           var result = await _ticketService.UpdateTicketStatusAsync(ticketId, status);
            if (result)
            {
                TempData[SuccessMessage] = "وضعیت تغییر یافت";
                return RedirectToAction("Index");
            }
            TempData[ErrorMessage] = "تغییر انجام نشد!";
            return View("Details");

        }
        #endregion

    }
}
