using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.Domain.Entities.Account;
using EShop.Domain.Entities.BaseEntities;
using EShop.Domain.Enums.TicketEnums;

namespace EShop.Domain.Entities.TicketSystem
{
    public class Ticket : BaseEntity
    {
        #region Properties
        public TicketSection Section { get; set; }
        public TicketPriority Priority { get; set; }
        public TicketStatus Status { get; set; }

        
        public string Title { get; set; }

        public int OwnerId { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsClosed { get; set; }
        #endregion

        #region Relations
        [ForeignKey("OwnerId")]
        public User Owner { get; set; }

        public ICollection<TicketMessage> TicketMessages { get; set; } = new List<TicketMessage>();
        #endregion
    }
}