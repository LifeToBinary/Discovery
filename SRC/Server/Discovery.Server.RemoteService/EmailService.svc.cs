using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Server.RemoteService
{
    /// <summary>
    /// 用于发送邮件
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="recipientAddress">收件人地址</param>
        /// <param name="subject">邮件标题</param>
        /// <param name="content">邮件内容</param>
        public void SendEmail(
            string recipientAddress,
            string subject,
            string content)
        {
            var from = new MailAddress(ConfigurationManager.AppSettings["EmailServiceUid"]);
            var to = new MailAddress(recipientAddress);
            SendEmailCore(new MailMessage(from, to)
            {
                Subject = subject,
                Body = content
            });
        }

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="message">消息内容</param>
        private void SendEmailCore(MailMessage message)
        {
            var smtpClient = new SmtpClient
            {
                Host = "smtp-mail.outlook.com",
                Port = 587,
                Credentials = new NetworkCredential
                {
                    UserName = ConfigurationManager.AppSettings["EmailServiceUid"],
                    Password = ConfigurationManager.AppSettings["EmailServicePwd"]
                },
                EnableSsl = true
            };
            smtpClient.Send(message);
        }
    }
}
