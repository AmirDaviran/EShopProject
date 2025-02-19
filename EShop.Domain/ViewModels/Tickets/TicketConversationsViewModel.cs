using EShop.Domain.Entities.Account;
using EShop.Domain.Entities.TicketSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
