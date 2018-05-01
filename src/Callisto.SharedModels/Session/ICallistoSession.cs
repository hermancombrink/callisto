using Callisto.SharedModels.Asset;
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
        /// Gets or sets the Assets
        /// </summary>
        IAssetsModule Assets { get; set; }

        /// <summary>
        /// Gets a value indicating whether IsAuthenticated
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Gets the UserName
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Gets the CurrentCompanyRef
        /// </summary>
        long CurrentCompanyRef { get; }

        /// <summary>
        /// Gets the EmailAddress
        /// </summary>
        string EmailAddress { get; }
    }
}
