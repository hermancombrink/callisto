using Callisto.Core.Storage;
using Callisto.Module.Assets.Interfaces;
using Callisto.Module.Assets.Repository;
using Callisto.SharedModels.Asset;
using Callisto.SharedModels.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

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
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoAssets(this IServiceCollection services,
        Action<DbContextOptionsBuilder> dbContextFactory)
        {
            services.AddDbContext<AssetDbContext>(dbContextFactory);
            services.TryAddTransient<IAssetsRepository, AssetsRepository>();
            services.TryAddTransient<IAssetsModule, AssetsModule>();


            return services;
        }

        /// <summary>
        /// The AddCallistoAssets
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="connectionString">The <see cref="string"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoAssets(this IServiceCollection services,
        string connectionString)
        {
            return AddCallistoAssets(services, options => options.UseSqlServer(connectionString));
        }
    }
}
