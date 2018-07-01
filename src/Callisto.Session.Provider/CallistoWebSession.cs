using Callisto.SharedKernel;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;

namespace Callisto.Session.Provider
{
    /// <summary>
    /// Defines the <see cref="CallistoWebSession" />
    /// </summary>
    public class CallistoWebSession : BaseSession
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallistoWebSession"/> class.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/></param>
        public CallistoWebSession(IServiceProvider serviceProvider, IHttpContextAccessor httpContextAccessor) : base(serviceProvider)
        {
            HttpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets a value indicating whether IsAuthenticated
        /// </summary>
        public override bool IsAuthenticated => HttpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        /// <summary>
        /// Gets the UserName
        /// </summary>
        public override string UserName => HttpContextAccessor.HttpContext?.User?.Identity?.Name ?? string.Empty;

        /// <summary>
        /// Gets the CurrentCompanyRef
        /// </summary>
        public override long CurrentCompanyRef
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

        public override string UserId
        {
            get
            {
                var claim = HttpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Type == CallistoJwtClaimTypes.Id);
                return claim?.Value ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets the EmailAddress
        /// </summary>
        public override string EmailAddress => HttpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(c => c.Properties.Any(x => x.Value == CallistoJwtClaimTypes.Email))?.Value ?? string.Empty;

        /// <summary>
        /// Gets the HttpContextAccessor
        /// </summary>
        public IHttpContextAccessor HttpContextAccessor { get; }
    }
}
