using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace NETCore.MailKitExtensions.SMTP.Impl
{
    public class SendMail : ISendMail
    {
        private readonly ILogger _logger;
        private readonly IMailKitProvider _mailKitProvider;

        public SendMail(ILogger<SendMail> logger,IMailKitProvider mailKitProvider)
        {
            _logger = logger;
            _mailKitProvider = mailKitProvider;
        }

        public void SendEmail(string subject, string message, bool isText)
        {
            SendEmail(subject, _mailKitProvider.Options.AddresseeEmailBucket, message, isText);
        }

        public void SendEmail(string subject, string toAddress, string message, bool isText)
        {
            SendEmail(subject, new List<string> { toAddress }, message, isText);
        }

        public void SendEmail(string subject, List<string> addresseeEmailBucket, string message, bool isText)
        {
            if (string.IsNullOrEmpty(_mailKitProvider.Options.SenderEmail))
            {
                throw new ArgumentNullException(nameof(_mailKitProvider.Options.SenderEmail));
            }

            if (addresseeEmailBucket == null)
            {
                throw new ArgumentNullException(nameof(addresseeEmailBucket));
            }

            var addresseeMailbox = new List<MailboxAddress>();
            addresseeEmailBucket.ForEach(l =>
            {
                addresseeMailbox.Add(MailboxAddress.Parse(l));
            });

            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(_mailKitProvider.Options.Sender, _mailKitProvider.Options.SenderEmail));
            mimeMessage.To.AddRange(addresseeMailbox);
            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart(isText ? TextFormat.Text : TextFormat.Html)
            {
                Text = message
            };

            try
            {
                using var smtpClient = _mailKitProvider.SmtpClient;
                smtpClient.Send(mimeMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        public void SendEmailAsync(string subject, string message, bool isText)
        {
            Task.Factory.StartNew(() =>
            {
                SendEmail(subject, _mailKitProvider.Options.AddresseeEmailBucket, message, isText);
            });
        }

        public void SendEmailAsync(string subject, string toAddress, string message, bool isText)
        {
            Task.Factory.StartNew(() =>
            {
                SendEmail(subject, new List<string> { toAddress }, message, isText);
            });
        }

        public void SendEmailAsync(string subject, List<string> addresseeEmailBucket, string message, bool isText)
        {
            Task.Factory.StartNew(() =>
            {
                SendEmail(subject, addresseeEmailBucket, message, isText);
            });
        }
    }
}
