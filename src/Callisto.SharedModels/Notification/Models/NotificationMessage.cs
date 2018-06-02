using Callisto.SharedModels.Notification.Enum;

namespace Callisto.SharedModels.Notification.Models
{
    /// <summary>
    /// Defines the <see cref="NotificationMessage" />
    /// </summary>
    public class NotificationMessage
    {
        /// <summary>
        /// Gets or sets the Request
        /// </summary>
        public NotificationRequestModel Request { get; set; }

        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public NotificationType Type { get; set; }
    }
}
