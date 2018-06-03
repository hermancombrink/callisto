using Callisto.SharedKernel;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Notification.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Notification
{
    /// <summary>
    /// Defines the <see cref="INotificationModule" />
    /// </summary>
    public interface INotificationModule
    {
        /// <summary>
        /// The SubmitNotificationRequest
        /// </summary>
        /// <param name="model">The <see cref="NotificationRequestModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> SubmitEmailNotification(NotificationRequestModel model, NotificationType type = NotificationType.None);

        /// <summary>
        /// The CreateSimpleMessage
        /// </summary>
        /// <param name="type">The <see cref="NotificationType"/></param>
        /// <param name="destination">The <see cref="string"/></param>
        /// <param name="tokens">The <see cref="Dictionary{string, string}"/></param>
        /// <returns>The <see cref="NotificationMessage"/></returns>
        NotificationMessage CreateSimpleMessage(NotificationType type, string destination, Dictionary<string, string> tokens = null);
    }
}
