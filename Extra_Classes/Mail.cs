using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace UtilityBookingSystem.Extra_Classes
{
    public class Mail
    {
        public static void Send_Mail(string Email, string Body, string Subject)
        {
            using (MailMessage mm = new MailMessage("anchal.chaudhary329@gmail.com", Email))
            {
                mm.Subject = Subject;
                mm.Body = Body;
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("anchal.chaudhary329@gmail.com", "@nch@l123");
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);
            }
        }
    }
}