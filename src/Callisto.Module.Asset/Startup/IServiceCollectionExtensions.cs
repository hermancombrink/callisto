using Callisto.SharedModels.Asset;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Callisto.Module.Assets.Startup
{
    /// <summary>
    /// Defines the <see cref="IServiceCollectionExtensions" />
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// The UseCallistoAuthentication
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="config">The <see cref="IConfiguration"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoAssets(this IServiceCollection services,
               IConfiguration config)
        {
            services.AddTransient<IAssetsModule, AssetsModule>();

            return services;
        }
    }
}
