using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace www.dagligkaffe.dk.Common
{
    public class Admin
    {
        public static class Notification
        {
            public static void Run(string from, string to, string cred, string subject, string body)
            {
                MailMessage mail = new MailMessage(from, to);
                SmtpClient client = new SmtpClient();
                client = new SmtpClient();
                client.Credentials = new NetworkCredential(cred, "Nostromo2503");
                client.Port = 25;
                client.EnableSsl = false;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                //client.UseDefaultCredentials = false;
                client.Host = "192.168.0.15";
                mail.Subject = subject;
                mail.IsBodyHtml = true;
                mail.Body = body;

                client.Send(mail);
            }
        }
    }
}
