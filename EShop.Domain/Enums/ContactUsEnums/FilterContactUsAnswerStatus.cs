using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Enums.ContactUsEnums;

public enum FilterContactUsAnswerStatus
{
    [Display(Name = "همه")]
    All,

    [Display(Name = "پاسخ داده شده")]
    Answered,

    [Display(Name = "پاسخ داده نشده")]
    NotAnswered
}