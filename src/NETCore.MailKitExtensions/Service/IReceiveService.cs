using System;
namespace NETCore.MailKitExtensions.Service
{
    public interface IReceiveService
    {
        void ReceiveUnreadEmail(Action<ReceiveEventMessage> action);
    }
}
