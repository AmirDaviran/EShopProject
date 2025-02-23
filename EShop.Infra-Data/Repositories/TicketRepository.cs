using EShop.Domain.Entities.TicketSystem;
using EShop.Domain.Enums.TicketEnums;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories
{
    public class TicketRepository(EShopDbContext _context) : ITicketRepository
    {

        #region Ticket Methods

        #region GetTicketById

        public async Task<Ticket> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Tickets
                .Include(t => t.Owner)
                .Include(t => t.TicketMessages)
                .ThenInclude(tm => tm.Sender)
                .FirstOrDefaultAsync(t => t.Id == ticketId && !t.IsDeleted);
        }

        #endregion

        #region GetTicketsByUserId

        public async Task<List<Ticket>> GetTicketsByUserIdAsync(int userId)
        {
            return await _context.Tickets
                .Where(t => t.OwnerId == userId && !t.IsDeleted)
                .ToListAsync();
        }

        #endregion#region MyRegion

        #region GetAllTickets
        public async Task<List<Ticket>> GetAllTicketsAsync()
        {
            return await _context.Tickets
                .Where(t => !t.IsDeleted)
                .Include(t => t.Owner)
                .ToListAsync();
        }

        #endregion

        #region AddTicket
        public async Task AddTicketAsync(Ticket ticket)
        {
            ticket.CreatedDate = DateTime.UtcNow;
            _context.Tickets.Add(ticket);
        }

        #endregion

        #region UpdateTicket
        public async Task UpdateTicketAsync(Ticket ticket)
        {
            ticket.UpdatedDate = DateTime.UtcNow;
            _context.Tickets.Update(ticket);
            await SaveChanges();
        }

        #endregion

        #region UpdateTicketStatus
        public async Task<bool> UpdateTicketStatusAsync(int ticketId, TicketStatus status)
        {
            var ticket = await GetTicketByIdAsync(ticketId);
            if (ticket == null) return false;

            ticket.Status = status;
            ticket.UpdatedDate = DateTime.UtcNow;
            await SaveChanges();
            return true;
        }

        #endregion

        #region CloseTicket
        public async Task<bool> CloseTicketAsync(int ticketId)
        {
            var ticket = await GetTicketByIdAsync(ticketId);
            if (ticket == null) return false;

            ticket.IsClosed = true;
            ticket.UpdatedDate = DateTime.UtcNow;
            await SaveChanges();
            return true;
        }

        #endregion

        #region SoftDeleteTicket
        public async Task<bool> SoftDeleteTicketAsync(int ticketId)
        {
            var ticket = await GetTicketByIdAsync(ticketId);
            if (ticket == null) return false;

            ticket.IsDeleted = true;
            ticket.UpdatedDate = DateTime.UtcNow;
            await SaveChanges();
            return true;
        }

        #endregion

        #endregion

        #region TicketMessage Methods

        #region GetMessagesByTicketId

        public async Task<List<TicketMessage>> GetMessagesByTicketIdAsync(int ticketId)
        {
            return await _context.TicketMessages
                .Where(tm => tm.TicketId == ticketId && !tm.IsDeleted)
                .Include(tm => tm.Sender)
                .OrderBy(tm => tm.CreatedDate)
                .ToListAsync();
        }

        #endregion

        #region AddMessageToTicket

        public async Task AddMessageToTicketAsync(TicketMessage message)
        {
            message.CreatedDate = DateTime.UtcNow;
            _context.TicketMessages.Add(message);
            await SaveChanges();
        }

        #endregion
        #endregion

        #region General Methods
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}