using System;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace NETCore.MailKitExtensions
{
    public class MailKitProvider : IMailKitProvider
    {
        public MailKitProvider(IOptions<MailKitOptions> options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));
            Options = options.Value;
        }

        public SmtpClient SmtpClient
        {
            get
            {
                return new Lazy<SmtpClient>(CreateSmtpClient).Value;
            }
        }

        public ImapClient ImapClient
        {
            get
            {
                return new Lazy<ImapClient>(CreateImapClient).Value;
            }
        }

        public MailKitOptions Options { get; }

        private SmtpClient CreateSmtpClient()
        {
            var smtpClient = new SmtpClient
            {
                ServerCertificateValidationCallback = (s, c, h, e) => true
            };

            smtpClient.Connect(Options.SmtpServer, Options.SmtpPort, Options.UseSsl);

            if (!string.IsNullOrEmpty(Options.Account) && !string.IsNullOrEmpty(Options.Password))
            {
                smtpClient.Authenticate(Options.Account, Options.Password);
            }

            return smtpClient;
        }

        private ImapClient CreateImapClient()
        {
            var imapClient = new ImapClient();

            imapClient.Connect(Options.ImapServer, Options.ImapPort);
            imapClient.Authenticate(Options.Account, Options.Password);

            return imapClient;
        }
    }
}
