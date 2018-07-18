using Callisto.Module.Locations.Interfaces;
using Callisto.Module.Locations.Repository;
using Callisto.SharedKernel;
using Callisto.SharedModels.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Data;
using System.Data.Common;

namespace Callisto.Module.Locations.Startup
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
        public static IServiceCollection AddCallistoLocations(this IServiceCollection services)
        {
            services.AddDbContext<LocationDbContext>((service, options) =>
            {
                var connection = service.GetRequiredService<IDbConnection>();
                options.UseSqlServer(connection as DbConnection);
            });

            services.TryAddScoped<ILocationRepository, LocationRepository>();
            services.TryAddScoped<ILocationModule, LocationModule>();


            return services;
        }

    }
}
