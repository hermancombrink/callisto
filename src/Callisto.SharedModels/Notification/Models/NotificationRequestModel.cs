using Callisto.SharedModels.Notification.Enum;
using System.Collections.Generic;

namespace Callisto.SharedModels.Notification.Models
{
    /// <summary>
    /// Defines the <see cref="NotificationRequestModel" />
    /// </summary>
    public class NotificationRequestModel
    {
        /// <summary>
        /// Gets or sets the Type
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Gets or sets the PreferredTransportType
        /// </summary>
        public TransportType PreferredTransportType { get; set; }

        /// <summary>
        /// Gets or sets the RequestedBy
        /// </summary>
        public string RequestedBy { get; set; }

        /// <summary>
        /// Gets or sets the DefaultContent
        /// </summary>
        public string DefaultContent { get; set; }

        /// <summary>
        /// Gets or sets the DefaultDestination
        /// </summary>
        public string DefaultDestination { get; set; }

        /// <summary>
        /// Gets or sets the DefaultSubject
        /// </summary>
        public string DefaultSubject { get; set; }

        /// <summary>
        /// Gets or sets the Tokens
        /// </summary>
        public IDictionary<string, string> Tokens { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// The Email
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <param name="subject">The <see cref="string"/></param>
        /// <param name="message">The <see cref="string"/></param>
        /// <returns>The <see cref="NotificationRequestModel"/></returns>
        public static NotificationRequestModel Email(string email, string subject, string message)
        {
            return new NotificationRequestModel()
            {
                DefaultDestination = email,
                DefaultSubject = subject,
                DefaultContent = message
            };
        }
    }
}
