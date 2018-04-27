using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;

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
        public IAuthenticationModule Authentication => ServiceProvider.GetRequiredService<IAuthenticationModule>();
    }
}
