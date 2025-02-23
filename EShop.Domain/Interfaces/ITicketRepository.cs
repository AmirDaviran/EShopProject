using EShop.Domain.Entities.TicketSystem;
using EShop.Domain.Enums.TicketEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> GetTicketByIdAsync(int ticketId);
        Task<List<Ticket>> GetTicketsByUserIdAsync(int userId);
        Task<List<Ticket>> GetAllTicketsAsync();
        Task AddTicketAsync(Ticket ticket);
       Task UpdateTicketAsync(Ticket ticket);
        Task<bool> UpdateTicketStatusAsync(int ticketId, TicketStatus status);
        Task<bool> CloseTicketAsync(int ticketId);
        Task<bool> SoftDeleteTicketAsync(int ticketId);

        Task<List<TicketMessage>> GetMessagesByTicketIdAsync(int ticketId);
        Task AddMessageToTicketAsync(TicketMessage message);
        Task SaveChanges();
    }
}
