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
            try
            {
                using (MailMessage mm = new MailMessage("akgechallbookingsystem@gmail.com", Email))
                {
                    mm.Subject = Subject;
                   // mm.Bcc.Add("aman.gupta232000@gmail.com");
                    mm.Body = Body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential("akgechallbookingsystem@gmail.com", "akashkool@123");
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}