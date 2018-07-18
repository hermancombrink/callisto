using Callisto.Module.Vendor;
using Callisto.Module.Vendor.Interfaces;
using Callisto.Module.Vendor.Repository;
using Callisto.SharedKernel;
using Callisto.SharedModels.Vendor;
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
        public static IServiceCollection AddCallistoVendor(this IServiceCollection services)
        {
            services.AddDbContext<VendorDbContext>((service, options) =>
            {
                var connection = service.GetRequiredService<IDbConnection>();
                options.UseSqlServer(connection as DbConnection);
            });

            services.TryAddScoped<IVendorRepository, VendorRepository>();
            services.TryAddScoped<IVendorModule, VendorModule>();

            return services;
        }
    }
}
