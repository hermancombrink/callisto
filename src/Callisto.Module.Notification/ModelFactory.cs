using Callisto.SharedModels.Notification.Models;

namespace Callisto.Module.Notification
{
    /// <summary>
    /// Defines the <see cref="ModelFactory" />
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// The CreateEmailFromNotification
        /// </summary>
        /// <param name="model">The <see cref="NotificationRequestModel"/></param>
        /// <returns>The <see cref="(string email, string subject, string message)"/></returns>
        public static (string email, string subject, string message) CreateEmailFromNotification(NotificationRequestModel model)
        {
            return (model.DefaultDestination, model.DefaultSubject, model.DefaultContent);
        }
    }
}
