using EShop.Domain.Entities.Account;
using EShop.Domain.Entities.TicketSystem;

namespace EShop.Domain.ViewModels.Tickets
{
    public class TicketConversationsViewModel
    {
        public int TicketId { get; set; }
        public User? User { get; set; }
        public List<TicketMessage>? TicketMessages { get; set; }
         //public bool IsUserMessage { get; set; } 
    }
}
