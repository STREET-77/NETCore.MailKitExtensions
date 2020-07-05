using System;
using System.Threading;
using NETCore.MailKitExtensions.Service;
using NETCore.MailKitExtensions.Service.Impl;
using Xunit;

namespace NETCore.MailKitExtensions.Tests
{
    public class SendTest
    {
        private readonly ISendService _send;

        public SendTest()
        {
            _send = new SendService(new MailKitProvider(new MailKitOptions
            {
            }));
        }

        [Fact]
        public void Send()
        {
        }
    }
}
