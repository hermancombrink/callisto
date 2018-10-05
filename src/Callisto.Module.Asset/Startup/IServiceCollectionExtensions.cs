using Callisto.Core.Storage;
using Callisto.Module.Assets.Interfaces;
using Callisto.Module.Assets.Repository;
using Callisto.SharedKernel;
using Callisto.SharedModels.Asset;
using Callisto.SharedModels.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Data;
using System.Data.Common;

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
        public static IServiceCollection AddCallistoAssets(this IServiceCollection services)
        {
            services.AddDbContext<AssetDbContext>((service, options) =>
            {
                var connection = service.GetRequiredService<IDbConnection>();
                options.UseSqlServer(connection as DbConnection);
            });
            services.TryAddScoped<IAssetsRepository, AssetsRepository>();
            services.TryAddScoped<IAssetsModule, AssetsModule>();

            return services;
        }
    }
}
