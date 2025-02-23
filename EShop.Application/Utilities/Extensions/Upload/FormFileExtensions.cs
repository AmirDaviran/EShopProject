using Microsoft.AspNetCore.Http;

namespace EShop.Application.Utilities.Extensions.Upload;

public static class FormFileExtensions
{
    private static readonly List<string> AllowedExtensions = new() { ".jpg", ".jpeg", ".png", ".pdf" };

    public static async Task<string?> SaveAttachmentAsync(this IFormFile? attachment, string uploadPath)
    {
        if (attachment == null) return null;

        var fileName = Guid.NewGuid() + Path.GetExtension(attachment.FileName);
        var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", uploadPath.TrimStart('/'));

        if (!Directory.Exists(fullPath))
            Directory.CreateDirectory(fullPath);

        var filePath = Path.Combine(fullPath, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await attachment.CopyToAsync(stream);
        }

        return $"/{uploadPath.TrimStart('/')}/{fileName}";
    }
}
