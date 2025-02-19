using EShop.Application.Interfaces;
using EShop.Application.Models;
using System.Net.Mail;

namespace EShop.Application.Services
{
    public class EmailService : IEmailService
    {

        #region Filelds

        //private readonly SendGridClient _sendGridClient;
        private readonly EmailSetting _emailSettings;
        //private readonly SendGridClient _sendGridClient;

        #endregion

        #region Constructor 

        public EmailService()
        {
           
           
        }

        #endregion


        #region Methods
        public void SendVerificationEmail(Email email)
        {
           
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("eshop1893@gmail.com", "EShop");
            mail.To.Add(email.To);
            mail.Subject = email.Subject;
            mail.Body = email.Body;
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;
            //SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("eshop1893@gmail.com", "tbuyimsgttkepylm");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        public void SendAdminResponseEmail(Email email)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("eshop1893@gmail.com", "EShop Admin");
            mail.To.Add(email.To);
            mail.Subject = email.Subject;
            mail.Body = email.Body;
            mail.IsBodyHtml = true;

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("eshop1893@gmail.com", "tbuyimsgttkepylm");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }

        #endregion
    }
}
