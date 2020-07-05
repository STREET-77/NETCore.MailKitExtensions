using System;
using MailKit;
using MailKit.Search;
using MimeKit;
using MimeKit.Text;

namespace NETCore.MailKitExtensions.Service.Impl
{
    public class ReceiveService : IReceiveService
    {
        private readonly IMailKitProvider _mailKitProvider;

        public ReceiveService(IMailKitProvider mailKitProvider)
        {
            _mailKitProvider = mailKitProvider;
        }

        public void ReceiveUnreadEmail(Action<ReceiveEventMessage> action)
        {
            using var imapClient = _mailKitProvider.ImapClient;
            imapClient.Inbox.Open(FolderAccess.ReadWrite);
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

                action.Invoke(receiveEventMessage);
            }
            imapClient.Inbox.Close();
        }
    }
}
