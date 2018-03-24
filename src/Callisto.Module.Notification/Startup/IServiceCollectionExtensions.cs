
using Callisto.Module.Notification.Email;
using Callisto.Module.Notification.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Module.Notification.Startup
{
    public static class IServiceCollectionExtensions
    {
        public static void AddSendGridNotificationModule(this IServiceCollection services, IConfiguration config, string configsection = "SendGrid")
        {
            services.Configure<MailOptions>(config.GetSection(configsection));
            services.AddTransient<IEmailSender, SimpleSendGridEmailSender>();
        }
    }
}
