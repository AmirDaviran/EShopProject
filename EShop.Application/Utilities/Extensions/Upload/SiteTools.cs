namespace EShop.Application.Utilities.Extensions.Upload;

public class SiteTools
{
    #region DefaultNames

    public static string DefaultImageName { get; set; }

    #endregion

    #region ContactUs

    public static string ContactUsAttachmentPath { get; set; } = "/Image/ContactUs/";
    public static string AdminResponseAttachmentPath { get; set; } = "/Image/AdminResponse/";
    public static string TicketAttachmentsPath { get; set; } = "/Image/TicketAttachments/";

    #endregion
}