using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Notification.Models;
using System.Threading.Tasks;

namespace Callisto.Module.Notification.Email
{
    /// <summary>
    /// Defines the <see cref="IEmailSender" />
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// The SendEmailAsync
        /// </summary>
        /// <param name="model">The <see cref="NotificationRequestModel"/></param>
        /// <param name="type">The <see cref="NotificationType"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task SendEmailAsync(NotificationRequestModel model, NotificationType type = NotificationType.None);
    }
}
