using System;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;

namespace NETCore.MailKitExtensions
{
    public interface IMailKitProvider
    {
        MailKitOptions Options { get; }
        SmtpClient SmtpClient { get; }
        ImapClient ImapClient { get; }
    }
}
