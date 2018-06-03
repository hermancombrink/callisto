using Callisto.Module.Notification.Email;
using Callisto.SharedKernel;
using Callisto.SharedModels.Notification;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Notification.Models;
using Callisto.SharedModels.Session;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
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
            IEmailSender emailSender,
            ICallistoSession session)
        {
            Logger = logger;
            EmailSender = emailSender;
            Session = session;
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
        /// Gets the Session
        /// </summary>
        public ICallistoSession Session { get; }

        /// <summary>
        /// The SubmitNotificationRequest
        /// </summary>
        /// <param name="model">The <see cref="NotificationRequestModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> SubmitEmailNotification(NotificationRequestModel model, NotificationType type = NotificationType.None)
        {
            return await RequestResult.From(async () =>
            {
                Logger.LogDebug($"Sending email to {model.DefaultDestination}");
                await EmailSender.SendEmailAsync(model, type);
            });
        }

        /// <summary>
        /// The CreateSimpleMessage
        /// </summary>
        /// <param name="type">The <see cref="NotificationType"/></param>
        /// <param name="destination">The <see cref="string"/></param>
        /// <param name="tokens">The <see cref="Dictionary{string, string}"/></param>
        /// <returns>The <see cref="NotificationMessage"/></returns>
        public NotificationMessage CreateSimpleMessage(NotificationType type, string destination, Dictionary<string, string> tokens = null)
        {
            return SimpleMessageFactory.CreateMessage(type, destination, tokens, Session.EmailAddress);
        }
    }
}
