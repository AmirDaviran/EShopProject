using Microsoft.AspNetCore.Http;

namespace EShop.Domain.ViewModels.ContactUsViewModel
{
    public class ContactUsDetailsViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string? AttachmentPath { get; set; }

        public IFormFile? Attachment { get; set; }

        public string? AdminAnswer { get; set; }

        public bool IsRead { get; set; }

        public bool IsReplied { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
