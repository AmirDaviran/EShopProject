using EShop.Application.Utilities.Tools;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.Utilities.Extensions.Upload;

public static class FormFileExtensions
{
    private static readonly List<string> AllowedExtensions = new() { ".jpg", ".jpeg", ".png", ".pdf" };

    public static async Task<string?> SaveAttachmentAsync(this IFormFile? attachment, string uploadPath)
    {
        if (attachment == null) return null;

        string fileExtension = Path.GetExtension(attachment.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(fileExtension)) return null;

        var fileName = Guid.NewGuid() + fileExtension;

        if (attachment.IsImage())
        {
            attachment.AddImageToServer(fileName, uploadPath);
        }
         else
        {
            await attachment.AddFilesToServer(fileName, uploadPath);
        }

        return fileName;
    }
}
