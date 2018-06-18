using Callisto.SharedModels.Asset;
using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Customer;
using Callisto.SharedModels.Location;
using Callisto.SharedModels.Messaging;
using Callisto.SharedModels.Notification;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Staff;
using Callisto.SharedModels.Vendor;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Callisto.Session.Provider
{
    /// <summary>
    /// Defines the <see cref="BaseSession" />
    /// </summary>
    public abstract class BaseSession : ICallistoSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSession"/> class.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/></param>
        public BaseSession(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// Gets a value indicating whether IsAuthenticated
        /// </summary>
        public abstract bool IsAuthenticated { get; }

        /// <summary>
        /// Gets the UserName
        /// </summary>
        public abstract string UserName { get; }

        /// <summary>
        /// Gets the CurrentCompanyRef
        /// </summary>
        public abstract long CurrentCompanyRef { get; }

        /// <summary>
        /// Gets the EmailAddress
        /// </summary>
        public abstract string EmailAddress { get; }

        /// <summary>
        /// Gets the ServiceProvider
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the MessageCoordinator
        /// </summary>
        public IMessageCoordinator MessageCoordinator => ServiceProvider.GetRequiredService<IMessageCoordinator>();

        /// <summary>
        /// Gets or sets the Authentication
        /// </summary>
        private IAuthenticationModule _authentication;

        /// <summary>
        /// Gets or sets the Authentication
        /// </summary>
        public IAuthenticationModule Authentication
        {
            get { return _authentication ?? (_authentication = ServiceProvider.GetRequiredService<IAuthenticationModule>()); }
            set { _authentication = value; }
        }

        /// <summary>
        /// Gets the Notification
        /// </summary>
        private INotificationModule _notification;

        /// <summary>
        /// Gets or sets the Notification
        /// </summary>
        public INotificationModule Notification
        {
            get { return _notification ?? (_notification = ServiceProvider.GetRequiredService<INotificationModule>()); }
            set { _notification = value; }
        }

        /// <summary>
        /// Defines the _assets
        /// </summary>
        private IAssetsModule _assets;

        /// <summary>
        /// Gets or sets the Assets
        /// </summary>
        public IAssetsModule Assets
        {
            get { return _assets ?? (_assets = ServiceProvider.GetRequiredService<IAssetsModule>()); }
            set { _assets = value; }
        }

        /// <summary>
        /// Defines the _location
        /// </summary>
        private ILocationModule _location;

        /// <summary>
        /// Gets or sets the Location
        /// </summary>
        public ILocationModule Location
        {
            get { return _location ?? (_location = ServiceProvider.GetRequiredService<ILocationModule>()); }
            set { _location = value; }
        }

        /// <summary>
        /// Defines the _staff
        /// </summary>
        private IStaffModule _staff;

        /// <summary>
        /// Gets or sets the Staff
        /// </summary>
        public IStaffModule Staff
        {
            get { return _staff ?? (_staff = ServiceProvider.GetRequiredService<IStaffModule>()); }
            set { _staff = value; }
        }

        /// <summary>
        /// Defines the _customer
        /// </summary>
        private ICustomerModule _customer;

        /// <summary>
        /// Gets or sets the Customer
        /// </summary>
        public ICustomerModule Customer
        {
            get { return _customer ?? (_customer = ServiceProvider.GetRequiredService<ICustomerModule>()); }
            set { _customer = value; }
        }

        /// <summary>
        /// Defines the _vendor
        /// </summary>
        private IVendorModule _vendor;

        /// <summary>
        /// Gets or sets the Vendor
        /// </summary>
        public IVendorModule Vendor
        {
            get { return _vendor ?? (_vendor = ServiceProvider.GetRequiredService<IVendorModule>()); }
            set { _vendor = value; }
        }
    }
}
