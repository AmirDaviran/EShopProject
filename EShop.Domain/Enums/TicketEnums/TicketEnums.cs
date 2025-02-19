using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Enums.TicketEnums
{
    #region Ticket Entity Enums
    public enum TicketPriority
    {
        [Display(Name = "کم")]
        Low,
        [Display(Name = "متوسط")]
        Medium,
        [Display(Name = "بالا")]
        High
    }
    public enum TicketStatus
    {
        [Display(Name = "باز")]
        Open,
        [Display(Name = "در حال رسیدگی")]
        InProgress,
        [Display(Name = "بسته شده")]
        Closed
    }
    public enum TicketSection
    {
        [Display(Name = "پشتیبانی")]
        TechnicalSupport,

        [Display(Name = "حسابداری")]
        Billing,
        [Display(Name = "فروش")]
        Sales,
        //[Display(Name = "")]
        //GeneralInquiry
    }
    #endregion


    #region Create Ticket Response Enums
    public enum CreateTicketResult
    {
        Success,
        IsNotImageOrPDF,
        FileUploaded,
        Failure
    }
    #endregion

    


}
