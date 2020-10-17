using System;

namespace NETCore.MailKitExtensions.IMAP
{
    public interface IReceiveMail
    {
        void ReceiveUnreadEmail(Action<ReceiveEventMessage> action);
    }
}
