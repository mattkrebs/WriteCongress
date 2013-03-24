using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace WriteCongress.Web.Controllers
{
    public class EmailManager
    {
        public static MailAddress Team = new System.Net.Mail.MailAddress("team@writecongress.us", "Matt, George and Joseph - WriteCongress.us");
        public static MailAddress Support = new System.Net.Mail.MailAddress("support@writecongress.us", "Matt - WriteCongress.us Support");
        public void SendMessage(string recipientEmail, MailAddress sender, string subject, string htmlBody) {
            
            htmlBody = htmlBody.Replace("${subject}", subject);
            MailMessage mm = new MailMessage();
            mm.To.Add(recipientEmail);
            if (sender == null)
            {
                mm.From = Support;
            }
            else
            {
                mm.From = sender;
            }
            mm.Subject = subject;
            mm.Body = htmlBody;
            mm.IsBodyHtml = true;

            var client = new SmtpClient();
            client.Send(mm);
        }
    }
}