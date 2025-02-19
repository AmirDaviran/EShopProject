

using Microsoft.AspNetCore.Http;

namespace EShop.Domain.ViewModels.ContactUsViewModel
{
    public class ContactUsAdminResponseViewModel
    {
        public int Id { get; set; }

        public string? AttachmentPath { get; set; }

        public IFormFile? Attachment { get; set; }


        public string AdminAnswer { get; set; }
    }
}
