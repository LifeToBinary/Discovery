using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Core.Tools
{
    /// <summary>
    /// 用于发送电子邮件
    /// </summary>
    public class EmailSender
    {
        /// <summary>
        /// 用户凭据用户名(邮箱地址)
        /// </summary>
        public string EmailAddressOfSender { get; set; }
        /// <summary>
        /// 用户凭据密码(邮箱密码)
        /// </summary>
        public string PasswordOfSender { get; set; }

        /// <summary>
        /// 以异步的方式发送消息
        /// </summary>
        /// <param name="message">消息内容</param>
        /// <returns>可等待的任务对象</returns>
        public Task SendEmailAsync(MailMessage message)
            => new SmtpClient
            {
                Host = "smtp-mail.outlook.com",
                Port = 587,
                Credentials = new NetworkCredential(EmailAddressOfSender, PasswordOfSender),
                EnableSsl = true
            }.SendMailAsync(message);

    }
}
