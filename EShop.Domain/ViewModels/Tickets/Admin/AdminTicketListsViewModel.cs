using EShop.Domain.Entities.Account;
using EShop.Domain.Enums.TicketEnums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.ViewModels.Tickets.Admin
{
    public class AdminTicketListsViewModel
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

        //public string OwnerName { get; set; }

        //public string OwnerLastName { get; set; }

        public User Owner {  get; set; } 
    }

}
