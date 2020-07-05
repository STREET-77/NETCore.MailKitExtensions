using System;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKitExtensions.Service.Impl;

namespace NETCore.MailKitExtensions.Service
{
    internal static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.Add(ServiceDescriptor.Scoped<ISendService, SendService>());
            services.Add(ServiceDescriptor.Scoped<IReceiveService, ReceiveService>());

            return services;
        }
    }
}
