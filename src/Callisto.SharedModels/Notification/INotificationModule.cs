using Callisto.SharedKernel;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Notification.Models;
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
    }
}
