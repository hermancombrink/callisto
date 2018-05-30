using Callisto.SharedKernel;
using Callisto.SharedModels.Asset;
using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Location;
using Callisto.SharedModels.Notification;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Staff;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Callisto.Session.Provider
{
    /// <summary>
    /// Defines the <see cref="CallistoSession" />
    /// </summary>
    public class CallistoSession : ICallistoSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallistoSession"/> class.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/></param>
        public CallistoSession(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor)
        {
            ServiceProvider = serviceProvider;
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets a value indicating whether IsAuthenticated
        /// </summary>
        public bool IsAuthenticated => HttpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        /// <summary>
        /// Gets the UserName
        /// </summary>
        public string UserName => HttpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;

        /// <summary>
        /// Gets the CurrentCompanyRef
        /// </summary>
        public long CurrentCompanyRef
        {
            get
            {
                var claim = HttpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == CallistoJwtClaimTypes.Company);
                if (claim != null)
                {
                    long.TryParse(claim.Value, out long companyRefId);
                    return companyRefId;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the EmailAddress
        /// </summary>
        public string EmailAddress => HttpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Properties.Any(x => x.Value == CallistoJwtClaimTypes.Email))?.Value ?? string.Empty;

        /// <summary>
        /// Gets the ServiceProvider
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// Gets the HttpContextAccessor
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; }

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
    }
}
