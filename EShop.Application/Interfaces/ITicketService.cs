using EShop.Domain.Entities.TicketSystem;
using EShop.Domain.Enums.TicketEnums;
using EShop.Domain.ViewModels.Tickets;
using EShop.Domain.ViewModels.Tickets.Admin;

namespace EShop.Application.Interfaces
{
    public interface ITicketService
    {
        
        Task<List<TicketListsViewModel>> GetAllTicketsForUserAsync(int userId);
        Task<CreateTicketResult> CreateTicketAsync(CreateTicketViewModel createTicket);
        Task UpdateTicketConversationAsync(UpdateTicketMessagesViewModel updateTicketMessages);

        #region Admin
        Task<Ticket> GetTicketByTicketIDAsync(int ticketId);
       Task<List<AdminTicketListsViewModel>> GetAllTicketsInAdminAsync();
        Task<List<TicketConversationsViewModel>> GetTicketConversationsByTicketIdAsync(int ticketId);
        Task<List<TicketListsViewModel>> GetAllTicketsAsync();
        Task<bool> DeleteTicketAsync(int ticketId);
        Task<bool> UpdateTicketStatusAsync(int ticketId, TicketStatus status);
        #endregion

    }
}
