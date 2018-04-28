using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Notification;

namespace Callisto.SharedModels.Session
{
    /// <summary>
    /// Defines the <see cref="ICallistoSession" />
    /// </summary>
    public interface ICallistoSession
    {
        /// <summary>
        /// Gets the Authentication
        /// </summary>
        IAuthenticationModule Authentication { get; set; }

        /// <summary>
        /// Gets the Notification
        /// </summary>
        INotificationModule Notification { get; set; }

        /// <summary>
        /// Gets a value indicating whether IsAuthenticated
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Gets the UserName
        /// </summary>
        string UserName { get; }
    }
}
