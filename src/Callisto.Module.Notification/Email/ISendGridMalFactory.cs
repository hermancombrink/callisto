using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Notification.Models;
using SendGrid.Helpers.Mail;

namespace Callisto.Module.Notification.Email
{
    /// <summary>
    /// Defines the <see cref="ISendGridMalFactory" />
    /// </summary>
    public interface ISendGridMalFactory
    {
        /// <summary>
        /// The CreateMessage
        /// </summary>
        /// <param name="type">The <see cref="NotificationType"/></param>
        /// <param name="model">The <see cref="NotificationRequestModel"/></param>
        /// <returns>The <see cref="SendGridMessage"/></returns>
        SendGridMessage CreateMessage(NotificationType type, NotificationRequestModel model);
    }
}
