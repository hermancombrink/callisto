using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Notification.Models;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Module.Notification
{
    /// <summary>
    /// Defines the <see cref="SimpleMessageFactory" />
    /// </summary>
    public static class SimpleMessageFactory
    {
        /// <summary>
        /// The CreateMessage
        /// </summary>
        /// <param name="type">The <see cref="NotificationType"/></param>
        /// <param name="destination">The <see cref="string"/></param>
        /// <param name="tokens">The <see cref="Dictionary{string, string}"/></param>
        /// <param name="currentUser">The <see cref="string"/></param>
        /// <returns>The <see cref="NotificationMessage"/></returns>
        public static NotificationMessage CreateMessage(NotificationType type,
            string destination,
            Dictionary<string, string> tokens,
            string currentUser)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("Notification Body");
            builder.AppendLine("");
            builder.AppendLine("Tokens");
            if (tokens != null)
            {
                foreach (var item in tokens)
                {
                    builder.AppendLine($"Key : {item.Key}");
                    builder.AppendLine($"  Value : {item.Value}");
                }
            }

            return new NotificationMessage()
            {
                Type = type,
                Request = new NotificationRequestModel()
                {
                    DefaultContent = builder.ToString(),
                    DefaultSubject = "Callisto",
                    Type = type,
                    Tokens = tokens,
                    DefaultDestination = destination,
                    PreferredTransportType = TransportType.Email,
                    RequestedBy = currentUser
                }
            };
        }
    }
}
