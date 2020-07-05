using System;
using NETCore.MailKitExtensions.Service;
using NETCore.MailKitExtensions.Service.Impl;
using Xunit;

namespace NETCore.MailKitExtensions.Tests
{
    public class ReceiveTest
    {
        private readonly IReceiveService _receive;

        public ReceiveTest()
        {
            _receive = new ReceiveService(new MailKitProvider(new MailKitOptions
            {
            }));
        }

        [Fact]
        public void Test1()
        {
            _receive.ReceiveUnreadEmail(message =>
            {
                Console.WriteLine(message.Content);
            });
        }
    }
}
