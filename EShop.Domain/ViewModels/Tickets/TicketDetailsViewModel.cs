using EShop.Domain.Entities.TicketSystem;
using EShop.Domain.Enums.TicketEnums;

namespace EShop.Domain.ViewModels.Tickets
{
    public class TicketDetailsViewModel
    {
        public Ticket? Ticket { get; set; }
        public List<TicketConversationsViewModel>? Conversations { get; set; }
        public DateTime UpdatedDate { get; set; }
        public TicketStatus Status { get; set; }
    }
}
