using Callisto.Module.Notification.Email;
using Callisto.Module.Notification.Options;
using Callisto.SharedKernel.Messaging;
using Callisto.SharedModels.Notification;
using Callisto.SharedModels.Notification.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Callisto.Module.Notification.Startup
{
    /// <summary>
    /// Defines the <see cref="IServiceCollectionExtensions" />
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// The AddSendGridNotificationModule
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="configsection">The <see cref="string"/></param>
        public static void AddCallistoNotification(this IServiceCollection services, MailOptions mailOptions)
        {
            services.AddTransient<IEmailSender, SimpleSendGridEmailSender>();
            services.AddSingleton<ISendGridMalFactory, SendGridMailFactory>();
            services.AddTransient<INotificationModule, NotificationModule>();

            services.AddSingleton(ConsumeBinding.SetBinding<NotificationMessage>("CallistoNotification"));
        }
    }
}
