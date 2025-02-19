using EShop.Domain.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace EShop.Domain.Entities.ContactUs;

public class ContactUs : BaseEntity
{

    [Required, MaxLength(150)]
    public string FirstName { get; set; }

    [Required, MaxLength(150)]
    public string LastName { get; set; }

    [Required, MaxLength(250), EmailAddress]
    public string Email { get; set; }

    [Required, MaxLength(15)]
    public string Mobile { get; set; }

    [Required, MaxLength(350)]
    public string Title { get; set; }

    [Required, MaxLength(1000)]
    public string Description { get; set; }

    public string? AttachmentPath { get; set; } // مسیر ذخیره فایل‌ها

    public string? AdminAnswer { get; set; }

    public bool IsRead { get; set; }

    public bool IsReplied { get; set; }

}
