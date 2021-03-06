﻿using System;
using System.Collections.Generic;

namespace NETCore.MailKitExtensions.SMTP
{
    public interface ISendMail
    {
        void SendEmail(string subject, string message, bool isText);
        void SendEmail(string subject, string toAddress, string message, bool isText);
        void SendEmail(string subject, List<string> addresseeEmailBucket, string message, bool isText);
        void SendEmailAsync(string subject, string message, bool isText);
        void SendEmailAsync(string subject, string toAddress, string message, bool isText);
        void SendEmailAsync(string subject, List<string> addresseeEmailBucket, string message, bool isText);
    }
}
