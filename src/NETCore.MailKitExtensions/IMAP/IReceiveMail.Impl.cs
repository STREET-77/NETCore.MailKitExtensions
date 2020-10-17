using System;
using MailKit;
using MailKit.Search;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace NETCore.MailKitExtensions.IMAP.Impl
{
    public class ReceiveMail : IReceiveMail
    {
        private readonly ILogger _logger;
        private readonly IMailKitProvider _mailKitProvider;

        public ReceiveMail(ILogger<ReceiveMail> logger, IMailKitProvider mailKitProvider)
        {
            _logger = logger;
            _mailKitProvider = mailKitProvider;
        }

        public void ReceiveUnreadEmail(Action<ReceiveEventMessage> action)
        {
            try
            {
                using var imapClient = _mailKitProvider.ImapClient;
                imapClient.Inbox.Open(FolderAccess.ReadWrite);
                // not seen
                foreach (var uid in imapClient.Inbox.Search(SearchQuery.NotSeen))
                {
                    var message = imapClient.Inbox.GetMessage(uid);
                    var receiveEventMessage = new ReceiveEventMessage
                    {
                        Subject = message.Subject,
                        From = ((MailboxAddress)message.From[0]).Address,
                        Content = message.GetTextBody(TextFormat.Text),
                        IsText = true
                    };

                    if (_mailKitProvider.Options.AutoSetSeenFlags)
                    {
                        imapClient.Inbox.SetFlags(uid, MessageFlags.Seen, true);
                    }
                    else
                    {
                        receiveEventMessage.OnSetRead += () =>
                        {
                            imapClient.Inbox.SetFlags(uid, MessageFlags.Seen, true);
                        };
                    }

                    _logger.LogInformation($"收到一封{receiveEventMessage.From}发来的邮件：{message.Subject}");
                    action.Invoke(receiveEventMessage);
                }
                imapClient.Inbox.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
