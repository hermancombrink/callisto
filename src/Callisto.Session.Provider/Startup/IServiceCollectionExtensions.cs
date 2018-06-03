using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Callisto.Session.Provider.Startup
{
    /// <summary>
    /// Defines the <see cref="IServiceCollectionExtensions" />
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// The UseCallistoSession
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoWebSession(this IServiceCollection services)
        {
            services.TryAddTransient<ICallistoSession, CallistoWebSession>();
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        /// <summary>
        /// The AddCallistoWorkerSession
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoWorkerSession(this IServiceCollection services)
        {
            services.TryAddTransient<ICallistoSession, CallistoWorkerSession>();
            return services;
        }
    }
}
