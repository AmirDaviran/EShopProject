using EShop.Application.Interfaces;
using EShop.Application.Utilities.Extensions;
using EShop.Domain.Entities.TicketSystem;
using EShop.Domain.Enums.TicketEnums;
using EShop.Domain.Interfaces;
using EShop.Domain.ViewModels.Tickets;
using EShop.Domain.ViewModels.Tickets.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace EShop.Application.Services
{
    public class TicketService : ITicketService
    {

        #region Constructor   
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITicketRepository _ticketRepository;
        public TicketService(ITicketRepository ticketRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _ticketRepository = ticketRepository;
        }
        #endregion



        #region Get All Tickets By UserID
        public async Task<List<TicketListsViewModel>> GetAllTicketsForUser(int userId)
        {
            var tickets = await _ticketRepository.GetTicketsByUserId(userId);
            List<TicketListsViewModel> ticketLists = new List<TicketListsViewModel>();

            foreach (var ticket in tickets)
            {
                ticketLists.Add(new TicketListsViewModel()
                {
                    TicketId = ticket.Id,
                    TicketTitle = ticket.Title,
                    Section = ticket.Section,
                    Status = ticket.Status,
                    UpdatedDate = ticket.UpdatedDate
                });
            }
            return ticketLists;
        }

        #endregion

        #region Upload Attachment File To Ticket
        public async Task<CreateTicketResult> UploadAttachmentFile(IFormFile attachmentFile, int ticketId, int ticketMessageId)
        {
            // Validate the uploaded file   
            if (!attachmentFile.IsImage() && !attachmentFile.IsPdf())
            {
                return CreateTicketResult.IsNotImageOrPDF;
            }

            var fileName = Guid.NewGuid().ToString("N") + Path.GetExtension(attachmentFile.FileName);
            var filePath = PathExtensions.AttachedFileOriginServer;

            // Call the method to add the file to the server  
            bool uploadSuccess = attachmentFile.AddFileToServer(fileName, filePath);

            if (uploadSuccess)
            {
                // Save the filename in the database   
                var attachedFile = new Attachment()
                {
                    FileName = fileName,
                    TicketId = ticketId,
                    TicketMessageId = ticketMessageId,
                    CreatedDate = DateTime.Now
                };
                await _ticketRepository.AddAttachment(attachedFile);
                return CreateTicketResult.FileUploaded;
            }
            else
                return CreateTicketResult.Failure; // Return failure if upload fails 
        }

        #endregion

        #region Create Ticket

        public async Task<CreateTicketResult> CreateTicket(CreateTicketViewModel createTicket)
        {
            // Step 1: Create a new Ticket and populate its properties from the ViewModel
            var ticket = new Ticket()
            {
                Title = createTicket.Title,
                Section = createTicket.Section,
                Priority = createTicket.Priority,
                Status = TicketStatus.Open,
                OwnerId = createTicket.OwnerId,
                UpdatedDate = DateTime.Now,
                CreatedDate = DateTime.Now,
                IsClosed = false
            };

            // Step 2: Add the Ticket to the database context  
            await _ticketRepository.AddTicket(ticket);
            await _ticketRepository.SaveChanges();

            // Step 3: Create the TicketMessage
            var message = new TicketMessage()
            {
                Message = createTicket.Description,
                TicketId = ticket.Id,
                CreatedDate = DateTime.Now,
                SenderId = _httpContextAccessor.HttpContext.User.GetUserID()
            };

            // Step 4: Add the TicketMessage to the context  
            await _ticketRepository.AddMessageToTicket(message);
            await _ticketRepository.SaveChanges();

            // Step 5: If you have attachments, handle them similarly  
            if (createTicket.AttachmentFile != null && createTicket.AttachmentFile.Length > 0)
            {
                var uploadResult = await UploadAttachmentFile(createTicket.AttachmentFile, ticket.Id, message.Id);
                if (uploadResult != CreateTicketResult.FileUploaded)
                {
                    return uploadResult;
                }

            }

            // Step 5: Save changes for messages and attachments  

            await _ticketRepository.SaveChanges();

            return CreateTicketResult.Success;
        }
        #endregion


        #region Update Ticket
        public async Task UpdateTicket(Ticket ticket)
        {
            await _ticketRepository.UpdateTicket(ticket);
        }
        #endregion

        #region Update Ticket Conversations
        public async Task UpdateTicketConversation(UpdateTicketMessagesViewModel updateTicketMessages)
        {
        
            if (!string.IsNullOrEmpty(updateTicketMessages.Message))
            {
                var message = new TicketMessage()
                {
                    Message = updateTicketMessages.Message,
                    TicketId = updateTicketMessages.TicketId,
                    CreatedDate = DateTime.Now,
                    SenderId = updateTicketMessages.SenderId
                    
                };
                var ticket = await GetTicketByTicketID(updateTicketMessages.TicketId);
                ticket.UpdatedDate = DateTime.Now;
                await _ticketRepository.AddMessageToTicket(message);
                await _ticketRepository.UpdateTicket(ticket);
                await _ticketRepository.SaveChanges();

                // If there is an attachment, handle the upload  
                if (updateTicketMessages.AttachmentFile != null && updateTicketMessages.AttachmentFile.Length > 0)
                {
                    var uploadResult = await UploadAttachmentFile(updateTicketMessages.AttachmentFile,
                                             updateTicketMessages.TicketId, message.Id);
                    if(uploadResult != CreateTicketResult.FileUploaded) 
                    {
                        throw new Exception("Attachment upload failed.");
                    }
                }
            }

            await _ticketRepository.SaveChanges();

        }


        #endregion



        #region Admin

        #region Get All Tickets By Owner
        public async Task<List<AdminTicketListsViewModel>> GetAllTicketsInAdmin()
        {
            var tickets = await _ticketRepository.GetAllTickets();

            var ticketLists = tickets.Select(t => new AdminTicketListsViewModel
            {
                TicketId = t.Id,
                TicketTitle = t.Title,
                Section = t.Section,
                Status = t.Status,
                CreatedDate = t.CreatedDate,
                UpdatedDate = t.UpdatedDate,
                Owner = t.Owner
            }).ToList();

            return ticketLists;
        }

        #endregion

        #region Get All Tickets
        public async Task<List<TicketListsViewModel>> GetAllTickets()
        {
            var tickets = await _ticketRepository.GetAllTickets();
            List<TicketListsViewModel> ticketLists = new List<TicketListsViewModel>();
            foreach (var ticket in tickets)
            {
                ticketLists.Add(new TicketListsViewModel()
                {
                    TicketId = ticket.Id,
                    TicketTitle = ticket.Title,
                    Section = ticket.Section,
                    Status = ticket.Status,
                    UpdatedDate = ticket.UpdatedDate
                });
            }
            return ticketLists;
        }


        #endregion

        #region Get Ticket By ID
        public async Task<Ticket> GetTicketByTicketID(int ticketId)
        {
            return await _ticketRepository.GetTicketById(ticketId);
        }

        #endregion


        #region Delete Ticket
        public async Task<bool> DeleteTicket(int ticketId)
        {
            return await _ticketRepository.SoftDeleteTicket(ticketId);
        }

        #endregion

        #region Get Ticket Conversations By Ticket ID
        public async Task<List<TicketConversationsViewModel>>
                                    GetTicketConversationsByTicketId(int ticketId)
        {
            var ticket = await GetTicketByTicketID(ticketId);
            if (ticket == null)
                return null;
            var responses = await _ticketRepository.GetMessagesByTicketId(ticketId);
            var conversations = new List<TicketConversationsViewModel>();
            //var userGroup = responses.GroupBy(r => new { r.SenderId, r.CreatedDate.Date })
            //    .OrderBy(g => g.Key.Date);
            var userGroup = responses.OrderBy(r => r.CreatedDate);


            foreach (var conversation in userGroup)
            {
                var message = new TicketConversationsViewModel()
                {
                    User = conversation.Sender,
                    TicketMessages = new List<TicketMessage> { conversation }
                };
                conversations.Add(message);
            }
            return conversations;


        }

        #endregion


        #region Update Ticket Status
        public async Task<bool> UpdateTicketStatus(int ticketId, TicketStatus status)
        {
            return await _ticketRepository.UpdateTicketStatus(ticketId, status);
        }

        #endregion

        #endregion
    }
}
