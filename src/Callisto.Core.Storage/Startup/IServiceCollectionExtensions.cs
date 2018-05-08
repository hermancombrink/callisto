using Callisto.Core.Storage.Options;
using Callisto.SharedModels.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Callisto.Core.Storage.Startup
{
    /// <summary>
    /// Defines the <see cref="IServiceCollectionExtensions" />
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// The AddCallistoMonitoring
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        public static void AddCallistoStorage(this IServiceCollection services, StorageOptions options)
        {
            services.TryAddTransient<IStorage, AzureBlobStorage>();
        }
    }
}
