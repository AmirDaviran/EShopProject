using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Enums.TicketEnums
{
    #region Ticket Entity Enums
    public enum TicketPriority
    {
        [Display(Name = "عادی")]
        Normal,
        [Display(Name = "مهم")]
        Important,
        [Display(Name = "خیلی مهم")]
        Critical
    }
    public enum TicketStatus
    {
        [Display(Name = "در حال بررسی")]
        Pending,
        [Display(Name = "پاسخ داده شد")]
        Answered,
        [Display(Name = "بسته")]
        Closed
    }
    public enum TicketSection
    {
        [Display(Name = "پشنهاد")]
        Suggestion,
        [Display(Name = "انتقاد یا شکایت")]
        Complaint,
        [Display(Name = "پیگیری سفارش")]
        OrderFollowUp,
        [Display(Name = "خدمات پس از فروش")]
        AfterSales,
        [Display(Name = "استعلام گارانتی")]
        Warranty,
        [Display(Name = "مدیریت")]
        Management,
        [Display(Name = "حسابداری و امور مالی")]
        Finance,
        [Display(Name = "سایر موضوعات")]
        Other
    }
    #endregion


    #region Create Ticket Response Enums
    public enum CreateTicketResult
    {
        Success,
        IsNotImageOrPDF,
        FileUploaded,
        Failure,
        EmptyMessage
    }
    #endregion

    


}
