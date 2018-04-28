using Callisto.Module.Notification.Email;
using Callisto.SharedKernel;
using Callisto.SharedModels.Notification;
using Callisto.SharedModels.Notification.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Callisto.Module.Notification
{
    /// <summary>
    /// Defines the <see cref="NotificationModule" />
    /// </summary>
    public class NotificationModule : INotificationModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationModule"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger{NotificationModule}"/></param>
        /// <param name="emailSender">The <see cref="IEmailSender"/></param>
        public NotificationModule(
            ILogger<NotificationModule> logger,
            IEmailSender emailSender)
        {
            Logger = logger;
            EmailSender = emailSender;
        }

        /// <summary>
        /// Gets the Logger
        /// </summary>
        private ILogger<NotificationModule> Logger { get; }

        /// <summary>
        /// Gets the EmailSender
        /// </summary>
        public IEmailSender EmailSender { get; }

        /// <summary>
        /// The SubmitNotificationRequest
        /// </summary>
        /// <param name="model">The <see cref="NotificationRequestModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> SubmitEmailNotification(NotificationRequestModel model)
        {
            return await RequestResult.From(async () =>
            {
                Logger.LogDebug($"Sending email to {model.DefaultDestination}");
                var (email, subject, message) = ModelFactory.CreateEmailFromNotification(model);
                await EmailSender.SendEmailAsync(email, subject, message);
            });
        }
    }
}
