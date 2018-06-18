using Callisto.SharedModels.Asset;
using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Customer;
using Callisto.SharedModels.Location;
using Callisto.SharedModels.Messaging;
using Callisto.SharedModels.Notification;
using Callisto.SharedModels.Staff;
using Callisto.SharedModels.Vendor;

namespace Callisto.SharedModels.Session
{
    /// <summary>
    /// Defines the <see cref="ICallistoSession" />
    /// </summary>
    public interface ICallistoSession
    {
        /// <summary>
        /// Defines the MessageCoordinator
        /// </summary>
        IMessageCoordinator MessageCoordinator { get; }

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
        /// Gets or sets the Location
        /// </summary>
        ILocationModule Location { get; set; }

        /// <summary>
        /// Gets or sets the Staff
        /// </summary>
        IStaffModule Staff { get; set; }

        /// <summary>
        /// Gets or sets the Vendor
        /// </summary>
        IVendorModule Vendor { get; set; }

        /// <summary>
        /// Gets or sets the Customer
        /// </summary>
        ICustomerModule Customer { get; set; }

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
