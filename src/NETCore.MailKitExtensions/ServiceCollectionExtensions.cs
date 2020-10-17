using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NETCore.MailKitExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMailKit(this IServiceCollection services, Action<MailKitOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            services.AddOptions();
            services.Configure(setupAction);
            services.Add(ServiceDescriptor.Scoped<IMailKitProvider, MailKitProvider>());
            services.Add(ServiceDescriptor.Scoped<IMAP.IReceiveMail, IMAP.Impl.ReceiveMail>());
            services.Add(ServiceDescriptor.Scoped<SMTP.ISendMail, SMTP.Impl.SendMail>());

            return services;
        }

        public static IServiceCollection AddMailKit(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailKitOptions>(configuration);
            services.Add(ServiceDescriptor.Scoped<IMailKitProvider, MailKitProvider>());
            services.Add(ServiceDescriptor.Scoped<IMAP.IReceiveMail, IMAP.Impl.ReceiveMail>());
            services.Add(ServiceDescriptor.Scoped<SMTP.ISendMail, SMTP.Impl.SendMail>());

            return services;
        }
    }
}
