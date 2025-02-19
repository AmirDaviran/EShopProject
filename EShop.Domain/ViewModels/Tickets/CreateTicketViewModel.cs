using EShop.Domain.Enums.TicketEnums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.ViewModels.Tickets
{
    public class CreateTicketViewModel
    {
        public TicketSection Section { get; set; }

        [Display(Name = "اولویت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public TicketPriority Priority { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public TicketStatus Status { get; set; }

        [Display(Name = "عنوان تیکت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        public string Title { get; set; }

        [Display(Name = "پیام تیکت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        public string Description { get; set; }

        public int OwnerId { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime? UpdatedDate { get; set; }
        public bool IsClosed { get; set; }
        public IFormFile? AttachmentFile { get; set; }
    }
}
