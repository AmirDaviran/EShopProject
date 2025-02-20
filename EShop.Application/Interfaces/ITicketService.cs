using EShop.Domain.Entities.TicketSystem;
using EShop.Domain.Enums.TicketEnums;
using EShop.Domain.ViewModels.Tickets;
using EShop.Domain.ViewModels.Tickets.Admin;

namespace EShop.Application.Interfaces
{
    public interface ITicketService
    {
       
        Task<List<TicketListsViewModel>> GetAllTicketsForUser(int userId);

        Task<CreateTicketResult> CreateTicket(CreateTicketViewModel createTicket);
        Task UpdateTicket (Ticket ticket);
        Task UpdateTicketConversation(UpdateTicketMessagesViewModel updateTicketMessages);


        #region Admin
        Task<Ticket> GetTicketByTicketID(int ticketId);
        Task<List<AdminTicketListsViewModel>> GetAllTicketsInAdmin();
        Task<List<TicketConversationsViewModel>> GetTicketConversationsByTicketId(int ticketId);
        Task<List<TicketListsViewModel>> GetAllTickets();
        Task<bool> DeleteTicket(int ticketId);
        Task<bool> UpdateTicketStatus(int ticketId, TicketStatus status);
        #endregion

    }
}
