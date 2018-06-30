using Callisto.Module.Vendor;
using Callisto.Module.Vendor.Interfaces;
using Callisto.Module.Vendor.Repository;
using Callisto.SharedModels.Vendor;
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
        public static IServiceCollection AddCallistoVendor(this IServiceCollection services,
        Action<DbContextOptionsBuilder> dbContextFactory)
        {
            services.AddDbContext<VendorDbContext>(dbContextFactory);
            services.TryAddTransient<IVendorRepository, VendorRepository>();
            services.TryAddTransient<IVendorModule, VendorModule>();

            return services;
        }

        /// <summary>
        /// The AddCallistoAssets
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="connectionString">The <see cref="string"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoVendor(this IServiceCollection services,
        string connectionString)
        {
            return AddCallistoVendor(services, options => options.UseSqlServer(connectionString));
        }
    }
}
