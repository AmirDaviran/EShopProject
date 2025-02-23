using EShop.Application.Interfaces;
using EShop.Application.Utilities.Extensions.Identity;
using EShop.Application.Utilities.Extensions.Upload;
using EShop.Domain.Entities.TicketSystem;
using EShop.Domain.Enums.TicketEnums;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Tickets;
using EShop.Domain.ViewModels.Tickets.Admin;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.Services
{
    public class TicketService(ITicketRepository _ticketRepository, IHttpContextAccessor _httpContextAccessor) : ITicketService
    {

        #region User Methods

        #region GetAllTicketsForUser

        public async Task<List<TicketListsViewModel>> GetAllTicketsForUserAsync(int userId)
        {
            var tickets = await _ticketRepository.GetTicketsByUserIdAsync(userId);
            return tickets.Select(ticket => new TicketListsViewModel
            {
                TicketId = ticket.Id,
                TicketTitle = ticket.Title,
                Section = ticket.Section,
                Status = ticket.Status,
                UpdatedDate = ticket.UpdatedDate,
                CreatedDate = ticket.CreatedDate
            }).ToList();
        }

        #endregion

        #region CreateTicket

        public async Task<CreateTicketResult> CreateTicketAsync(CreateTicketViewModel createTicket)
        {
            var ticket = new Ticket
            {
                Title = createTicket.Title,
                Section = createTicket.Section,
                Priority = createTicket.Priority,
                Status = TicketStatus.Pending,
                OwnerId = createTicket.OwnerId,
                UpdatedDate = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                IsClosed = false
            };

            await _ticketRepository.AddTicketAsync(ticket);
            await _ticketRepository.SaveChanges();

            var message = new TicketMessage
            {
                Message = createTicket.Description,
                TicketId = ticket.Id,
                SenderId = _httpContextAccessor.HttpContext.User.GetUserID(),
                CreatedDate = DateTime.UtcNow
            };

            if (createTicket.AttachmentFile != null && createTicket.AttachmentFile.Length > 0)
            {
                var fileName = await createTicket.AttachmentFile.SaveAttachmentAsync(SiteTools.TicketAttachmentsPath);
                if (fileName != null)
                {
                    message.FileName = fileName;
                    message.FilePath = SiteTools.TicketAttachmentsPath + fileName;
                }
            }

            await _ticketRepository.AddMessageToTicketAsync(message);
            return CreateTicketResult.Success;
        }

        #endregion

        #region UpdateTicketConversation
        public async Task UpdateTicketConversationAsync(UpdateTicketMessagesViewModel updateTicketMessages)
        {
            if (string.IsNullOrEmpty(updateTicketMessages.Message)) return;

            var message = new TicketMessage
            {
                Message = updateTicketMessages.Message,
                TicketId = updateTicketMessages.TicketId,
                SenderId = updateTicketMessages.SenderId,
                CreatedDate = DateTime.UtcNow
            };

            if (updateTicketMessages.AttachmentFile != null && updateTicketMessages.AttachmentFile.Length > 0)
            {
                var fileName = await updateTicketMessages.AttachmentFile.SaveAttachmentAsync(SiteTools.TicketAttachmentsPath);
                if (fileName != null)
                {
                    message.FileName = fileName;
                    message.FilePath = SiteTools.TicketAttachmentsPath + fileName;
                }
            }

            var ticket = await _ticketRepository.GetTicketByIdAsync(updateTicketMessages.TicketId);
            ticket.UpdatedDate = DateTime.UtcNow;

            await _ticketRepository.AddMessageToTicketAsync(message);
            await _ticketRepository.UpdateTicketAsync(ticket);
        }

        #endregion

        #endregion

        #region Admin Methods

        #region GetAllTicketsInAdmin

        public async Task<List<AdminTicketListsViewModel>> GetAllTicketsInAdminAsync()
        {
            var tickets = await _ticketRepository.GetAllTicketsAsync();
            return tickets.Select(t => new AdminTicketListsViewModel
            {
                TicketId = t.Id,
                Subject = t.Title,
                Priority = t.Priority,
                Section = t.Section,
                Status = t.Status,
                CreatedDate = t.CreatedDate,
                UpdatedDate = t.UpdatedDate,
                Owner = t.Owner
            }).ToList();
        }

        #endregion

        #region GetAllTickets

        public async Task<List<TicketListsViewModel>> GetAllTicketsAsync()
        {
            return await GetAllTicketsForUserAsync(0); // 0 برای گرفتن همه تیکت‌ها بدون فیلتر کاربر
        }

        #endregion

        #region GetTicketByTicketID

        public async Task<Ticket> GetTicketByTicketIDAsync(int ticketId)
        {
            return await _ticketRepository.GetTicketByIdAsync(ticketId);
        }

        #endregion

        #region DeleteTicket

        public async Task<bool> DeleteTicketAsync(int ticketId)
        {
            return await _ticketRepository.SoftDeleteTicketAsync(ticketId);
        }

        #endregion

        #region GetTicketConversationsByTicketId

        public async Task<List<TicketConversationsViewModel>> GetTicketConversationsByTicketIdAsync(int ticketId)
        {
            var ticket = await GetTicketByTicketIDAsync(ticketId);
            if (ticket == null) return new List<TicketConversationsViewModel>();

            var messages = await _ticketRepository.GetMessagesByTicketIdAsync(ticketId);
            return messages.Select(m => new TicketConversationsViewModel
            {
                TicketId = ticketId,
                User = m.Sender,
                TicketMessages = new List<TicketMessage> { m }
            }).ToList();
        }

        #endregion

        #region UpdateTicketStatus

        public async Task<bool> UpdateTicketStatusAsync(int ticketId, TicketStatus status)
        {
            return await _ticketRepository.UpdateTicketStatusAsync(ticketId, status);
        }

        #endregion

        #endregion
    }
}