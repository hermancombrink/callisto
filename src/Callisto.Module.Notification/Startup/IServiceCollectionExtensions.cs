using Callisto.Module.Notification.Email;
using Callisto.Module.Notification.Options;
using Callisto.SharedModels.Notification;
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
        /// <param name="config">The <see cref="IConfiguration"/></param>
        /// <param name="configsection">The <see cref="string"/></param>
        public static void UseCallistNotification(this IServiceCollection services, IConfiguration config, MailOptions options)
        {
            services.AddTransient<IEmailSender, SimpleSendGridEmailSender>();
            services.AddTransient<INotificationModule, NotificationModule>();
        }
    }
}
