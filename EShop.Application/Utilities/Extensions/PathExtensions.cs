
namespace EShop.Application.Utilities.Extensions
{
    public static class PathExtensions
    {
        #region Ticket Attachments
        public static string AttachedFileOrigin = "/img/Ticket-Attachments/";
        //public static string AttachedFileOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "/img/Ticket-Attachments/");
        public static string AttachedFileOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Ticket-Attachments");
        #endregion

        #region Product Images
        public static string ProductOrigin = "/img/Product-Images/Origin/";
        //public static string ProductOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Product-Images", "Origin");
        public static string ProductOriginServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Product-Images/Origin/");
        public static string ProductThumb = "/img/Product-Images/Thumb/";
        //public static string ProductThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "Product-Images", "Thumb");
        public static string ProductThumbServer = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/Product-Images/Thumb/");
        #endregion
    }
}
