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
        Task<Ticket> GetTicketById(int ticketId);
        Task<List<Ticket>> GetTicketsByUserId(int userId);
        Task<List<Ticket>> GetAllTickets();
        Task<List<Ticket>> GetTicketsByStatus(TicketStatus status);
        Task SendTicket(Ticket ticket);
        Task AddTicket(Ticket ticket);
       Task UpdateTicket(Ticket ticket);
        Task<bool> UpdateTicketStatus(int ticketId, TicketStatus status);
        Task<bool> CloseTicket(int ticketId);
        Task<bool> SoftDeleteTicket(int ticketId);

        Task<List<TicketMessage>> GetMessagesByTicketId(int ticketId);
        Task AddMessageToTicket(TicketMessage message);

        Task AddAttachment(Attachment attachment);

        Task SaveChanges();
    }
}
