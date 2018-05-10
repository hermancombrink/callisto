﻿using Callisto.Module.Locations.Interfaces;
using Callisto.Module.Locations.Repository;
using Callisto.SharedModels.Location;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

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
        public static IServiceCollection AddCallistoLocations(this IServiceCollection services,
        Action<DbContextOptionsBuilder> dbContextFactory)
        {
            services.AddDbContext<LocationDbContext>(dbContextFactory);
            services.TryAddTransient<ILocationRepository, LocationRepository>();
            services.TryAddTransient<ILocationModule, LocationModule>();


            return services;
        }

        /// <summary>
        /// The AddCallistoAssets
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="connectionString">The <see cref="string"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoLocations(this IServiceCollection services,
        string connectionString)
        {
            return AddCallistoLocations(services, options => options.UseSqlServer(connectionString));
        }
    }
}
