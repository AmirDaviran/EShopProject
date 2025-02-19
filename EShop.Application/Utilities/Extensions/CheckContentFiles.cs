using Microsoft.AspNetCore.Http;



namespace EShop.Application.Utilities.Extensions
{

    public static class CheckContentFiles
    {
   

        private static readonly HashSet<string> ValidImageExtensions = new HashSet<string>
    {
        ".jpg",
        ".jpeg",
        ".png",
        ".gif",
        ".bmp",
        ".tiff"
    };

        public static bool IsImage(this IFormFile file)
        { 
            var extension = Path.GetExtension(file.FileName).ToLower();
            return ValidImageExtensions.Contains(extension);
        }

        public static bool IsPdf(this IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            return extension == ".pdf";
        }
    }
}
