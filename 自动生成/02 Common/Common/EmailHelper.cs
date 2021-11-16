using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common
{
    /// <summary>
    /// 
    /// </summary>
    public class EmailHelper
    {
        public static void Send(string smtp, string point, string acct, string pwd, string to, string title, string body)
        {
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = smtp;
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(acct, pwd);
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            message.To.Add(to);
            message.From = new System.Net.Mail.MailAddress(acct, acct, System.Text.Encoding.UTF8);
            message.Subject = title;
            message.Body = body;
            message.BodyEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            message.SubjectEncoding = System.Text.Encoding.GetEncoding("UTF-8");
            message.IsBodyHtml = false;
            message.Priority = System.Net.Mail.MailPriority.High;
            client.Send(message);

        }
    }
}
