using EShop.Domain.Enums;
using EShop.Domain.Enums.TicketEnums;
using System.ComponentModel.DataAnnotations;


namespace EShop.Domain.ViewModels.Tickets
{
    public class TicketListsViewModel
    {
        public int TicketId { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public DateTime? CreatedDate { get; set; }


        [Display(Name = "بخش")]
        public TicketSection Section { get; set; }

        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        [MaxLength(100, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد.")]
        public string TicketTitle { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید.")]
        public TicketStatus Status { get; set; }


        [Display(Name = "آخرین بروزرسانی")]
        public DateTime? UpdatedDate { get; set; }

    }
}
