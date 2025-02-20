using EShop.Application.Interfaces;
using EShop.Application.Utilities.Extensions.Identity;
using EShop.Domain.Enums.TicketEnums;
using EShop.Domain.ViewModels.Tickets;
using Microsoft.AspNetCore.Mvc;

namespace EShop.Web.Areas.Users.Controllers
{
    public class TicketController : UserBaseController
    {

        #region Constructor
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }
        #endregion

        public async Task<IActionResult> Index()
        {
            var userId = User.GetUserID();
            var ticketLists = await _ticketService.GetAllTicketsForUser(userId);
            return View(ticketLists);
        }

        [HttpGet]
        public IActionResult CreateTicket()
        {
            CreateTicketViewModel model = new CreateTicketViewModel()
            {
                OwnerId = User.GetUserID()
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


        [HttpGet]
        [Route("Users/Ticket/Details/{ticketId}")]
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

        [HttpPost]
        public async Task<IActionResult> UpdateTicketConversation(UpdateTicketMessagesViewModel updateTicketMessages)
        {
            if (!ModelState.IsValid)
                return View("Details");
            await _ticketService.UpdateTicketConversation(updateTicketMessages);
            return RedirectToAction("Details", new { ticketId = updateTicketMessages.TicketId });
        }

    }
}
