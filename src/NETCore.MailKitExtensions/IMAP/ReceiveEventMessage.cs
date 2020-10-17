using System;
namespace NETCore.MailKitExtensions.IMAP
{
    internal delegate void SetReadHandler();

    public class ReceiveEventMessage
    {
        public string Subject { get; set; }
        public string From { get; set; }
        public string Content { get; set; }
        public bool IsText { get; set; }

        internal event SetReadHandler OnSetRead;

        public void SetRead() => OnSetRead();
    }
}
