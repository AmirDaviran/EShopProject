using Microsoft.AspNetCore.Http;

namespace EShop.Domain.ViewModels.Tickets
{
    public class UpdateTicketMessagesViewModel
    {
        public int TicketId { get; set; }
        public string? Message { get; set; }
        public int SenderId { get; set; }
        public IFormFile? AttachmentFile { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}

