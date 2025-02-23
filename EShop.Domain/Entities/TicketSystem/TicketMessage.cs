using EShop.Domain.Entities.Account;
using EShop.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.Domain.Entities.TicketSystem
{
    public class TicketMessage : BaseEntity
    {
        #region Properties
        [Required(ErrorMessage = "متن پیام الزامی است")]
        public string Message { get; set; }

        public int TicketId { get; set; }
        public int SenderId { get; set; }

        [MaxLength(255, ErrorMessage = "نام فایل نمی‌تواند بیشتر از 255 کاراکتر باشد")]
        public string? FileName { get; set; } // نام فایل ضمیمه

        [MaxLength(500, ErrorMessage = "مسیر فایل نمی‌تواند بیشتر از 500 کاراکتر باشد")]
        public string? FilePath { get; set; } // مسیر ذخیره‌سازی فایل در سرور
        #endregion

        #region Relations
        [ForeignKey("TicketId")]
        public Ticket Ticket { get; set; }

        [ForeignKey("SenderId")]
        public User Sender { get; set; }
        #endregion
    }
}