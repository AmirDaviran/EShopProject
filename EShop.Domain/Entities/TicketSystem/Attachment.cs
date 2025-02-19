using EShop.Domain.Entities.BaseEntities;

namespace EShop.Domain.Entities.TicketSystem
{
    public class Attachment : BaseEntity
    {
        #region Properties

        public int TicketId { get; set; }
        public int TicketMessageId { get; set; }
        public string? FileName { get; set; }
        #endregion

        #region Relations
        public Ticket? Ticket { get; set; }
        public TicketMessage? TicketMessage { get; set; }

     
        #endregion
    }
}
