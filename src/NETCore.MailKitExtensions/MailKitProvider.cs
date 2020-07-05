using System;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;

namespace NETCore.MailKitExtensions
{
    public class MailKitProvider : IMailKitProvider
    {
        public MailKitProvider(IOptions<MailKitOptions> options)
        {
            Options = options.Value;
        }

        public SmtpClient SmtpClient
        {
            get
            {
                return new Lazy<SmtpClient>(InitSmtpClient).Value;
            }
        }

        public ImapClient ImapClient
        {
            get
            {
                return new Lazy<ImapClient>(InitImapClient).Value;
            }
        }

        public MailKitOptions Options { get; }

        private SmtpClient InitSmtpClient()
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

        private ImapClient InitImapClient()
        {
            var imapClient = new ImapClient();

            imapClient.Connect(Options.ImapServer, Options.ImapPort);
            imapClient.Authenticate(Options.Account, Options.Password);

            return imapClient;
        }
    }
}
