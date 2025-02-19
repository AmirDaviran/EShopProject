using EShop.Domain.Entities.TicketSystem;
using EShop.Domain.Enums.TicketEnums;
using EShop.Domain.Interfaces;
using EShop.Infra_Data.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infra_Data.Repositories
{
    public class TicketRepository : ITicketRepository
    {

        #region Constructor

        private readonly EShopDbContext _context;
        public TicketRepository(EShopDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Get Ticket By ID
        public async Task<Ticket> GetTicketById(int ticketId)
        {
            return await _context.Tickets
                .Include(t => t.Owner)
                .Include(t => t.TicketMessages)
                .ThenInclude(tm => tm.Attachments)
                .FirstOrDefaultAsync(t => t.Id == ticketId);
        }
        #endregion

        #region Get Tickets By UserID
        public async Task<List<Ticket>> GetTicketsByUserId(int userId)
        {
            
            return await _context.Tickets
                .Where(t => t.OwnerId == userId && !t.IsDeleted).ToListAsync();
        }

        #endregion

        #region Get All Tickets
        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _context.Tickets
                .Where(t => !t.IsDeleted)
                .Include(t => t.Owner).ToListAsync();
        }

        #endregion


        #region Get Tickets By Status
        public async Task<List<Ticket>> GetTicketsByStatus(TicketStatus status)
        {
            List<Ticket> tickets = await _context.Tickets.Where(t => t.Status == status).ToListAsync();
            tickets.ForEach(async t =>
            {
                t.Owner = await _context.Users.FirstOrDefaultAsync(u => u.Id == t.OwnerId);
                t.Attachments = await _context.Attachments.Where(a => a.TicketId == t.Id).ToListAsync();    
            });
            return tickets;
        }

        #endregion

        #region Add Ticket
        public async Task AddTicket(Ticket ticket)
        {
             _context.Tickets.Add(ticket);
        }
        #endregion

        #region Create Ticket
        public async Task SendTicket(Ticket ticket)
        {
           await AddTicket(ticket);
           await SaveChanges();
        }

        #endregion



        #region Update Ticket
        public async Task UpdateTicket(Ticket ticket)
        {
            _context.Tickets.Update(ticket);
        }
        #endregion


        #region Update Ticket Status
        public async Task<bool> UpdateTicketStatus(int ticketId, TicketStatus status)
        {
           Ticket ticket = await GetTicketById(ticketId);
            if (ticket != null)
            {
                ticket.Status = status;
                await SaveChanges();
                return true;
            }
            else
            return false;
        }

        #endregion

        #region Close a Ticket
        public async Task<bool> CloseTicket(int ticketId)
        {
            var ticket = await GetTicketById(ticketId);
            if (ticket != null)
            {
                ticket.IsClosed = true;
                return true;
            }
            return false;
        }

        #endregion

        #region Soft Delete Ticket
        public async Task<bool> SoftDeleteTicket(int ticketId)
        {
            var ticket = await GetTicketById(ticketId);
            if(ticket != null)
            {
                ticket.IsDeleted = true;
                ticket.UpdatedDate = DateTime.UtcNow;
                await SaveChanges();
                return true;
            }
            else
            return false;
        }

        #endregion

        #region Add a Message to a Ticket
        public async Task AddMessageToTicket(TicketMessage message)
        {
             _context.TicketMessages.Add(message);
        }
        #endregion

        #region Get Messages By TicketId 
        public async Task<List<TicketMessage>> GetMessagesByTicketId(int ticketId)
        {
            return await _context.TicketMessages
            .Where(tm => tm.TicketId == ticketId)
            .Include(tm => tm.Attachments)
            .Include(tm => tm.Sender)
            .OrderBy(tm => tm.CreatedDate)
            .ToListAsync();
        }

        #endregion

        #region Add Attachment 

        public async Task AddAttachment(Attachment attachment)
        {
             _context.Attachments.Add(attachment);
        }

        #endregion

        #region Save Changes
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

       
        #endregion

    }
}
