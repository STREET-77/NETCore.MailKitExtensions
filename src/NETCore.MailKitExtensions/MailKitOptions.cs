using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace NETCore.MailKitExtensions
{
    public class MailKitOptions : IOptions<MailKitOptions>
    {
        /// <summary>
        /// 寄信人
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// 寄信人邮箱
        /// </summary>
        public string SenderEmail { get; set; }

        /// <summary>
        /// 收信人邮箱
        /// </summary>
        public List<string> AddresseeEmailBucket { get; set; }

        /// <summary>
        /// 认证帐号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 认证密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// smtp
        /// 默认：smtp.163.com
        /// </summary>
        public string SmtpServer { get; set; } = "smtp.163.com";

        /// <summary>
        /// smtp port
        /// 默认：25 994/465(ssl)
        /// </summary>
        public int SmtpPort { get; set; } = 25;

        /// <summary>
        /// imap
        /// </summary>
        public string ImapServer { get; set; } = "imap.qq.com";

        /// <summary>
        /// imap port
        /// 默认：143 993(ssl)
        /// </summary>
        public int ImapPort { get; set; } = 143;

        /// <summary>
        /// 
        /// </summary>
        public bool UseSsl { get; set; } = false;

        /// <summary>
        /// 自动设置邮件为已读
        /// </summary>
        public bool AutoSetSeenFlags { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public MailKitOptions Value
        {
            get
            {
                return this;
            }
        }
    }
}
