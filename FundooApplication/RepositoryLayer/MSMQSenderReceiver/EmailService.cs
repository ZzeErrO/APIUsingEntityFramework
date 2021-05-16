using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Experimental.System.Messaging;

namespace RepositoryLayer.MSMQSenderReceiver
{
    public class EmailService
    {
        /// <summary>
        /// Emails the specified email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="token">The token.</param>
        public static void Email(string email, string body)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("i.am.testing.test123@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "Test";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("i.am.testing.test123@gmail.com", "Testing01@");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
