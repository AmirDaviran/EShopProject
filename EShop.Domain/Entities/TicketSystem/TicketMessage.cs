using EShop.Domain.Entities.Account;
using EShop.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Entities.TicketSystem
{
    public class TicketMessage : BaseEntity
    {
        #region Properties 
        public string Message { get; set; }
        public int TicketId { get; set; }
        public int SenderId { get; set; }
   
    
        #endregion

        #region Relations
        public Ticket Ticket { get; set; }
        public ICollection<Attachment>? Attachments { get; set; }

        [ForeignKey("SenderId")]
        public User Sender {  get; set; }
        #endregion
    }
}
