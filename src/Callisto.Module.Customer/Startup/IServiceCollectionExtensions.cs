using Callisto.Module.Customer;
using Callisto.Module.Customer.Interfaces;
using Callisto.Module.Customer.Repository;
using Callisto.SharedKernel;
using Callisto.SharedModels.Customer;
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
        public static IServiceCollection AddCallistoCustomer(this IServiceCollection services)
        {
            services.AddDbContext<CustomerDbContext>((service, options) =>
            {
                var connection = service.GetRequiredService<IDbConnection>();
                options.UseSqlServer(connection as DbConnection);
            });

            services.TryAddScoped<ICustomerRepository, CustomerRepository>();
            services.TryAddScoped<ICustomerModule, CustomerModule>();

            return services;
        }
    }
}
