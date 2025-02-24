namespace EShop.Application.Utilities.Extensions.Upload;

public class SiteTools
{
    #region DefaultNames

    public static string DefaultImageName { get; set; }

    #endregion

    #region PAth

    public static string ContactUsAttachmentPath { get; set; } = "/wwwroot/Image/ContactUs/";
    public static string AdminResponseAttachmentPath { get; set; } = "/wwwroot/Image/AdminResponse/";
    public static string TicketAttachmentsPath { get; set; } = "/wwwroot/Image/TicketAttachments/";
    public static string FAQCategoryAttachmentsPath { get; set; } = "/wwwroot/Image/FAQCategory/";
    public static string ProducMainPicturetAttachmentsPath { get; set; } = "/wwwroot/Image/Product/MainPicture";
    
    #endregion
}