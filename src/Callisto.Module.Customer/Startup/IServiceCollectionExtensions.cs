using Callisto.Module.Customer;
using Callisto.Module.Customer.Interfaces;
using Callisto.Module.Customer.Repository;
using Callisto.SharedKernel;
using Callisto.SharedModels.Customer;
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
        public static IServiceCollection AddCallistoCustomer(this IServiceCollection services,
        Action<DbContextOptionsBuilder> dbContextFactory)
        {
            services.AddDbContext<CustomerDbContext>(dbContextFactory);
            services.TryAddTransient<ICustomerRepository, CustomerRepository>();
            services.TryAddTransient<ICustomerModule, CustomerModule>();

            return services;
        }

        /// <summary>
        /// The AddCallistoAssets
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="connectionString">The <see cref="string"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoCustomer(this IServiceCollection services,
        string connectionString)
        {
            return AddCallistoCustomer(services, options => options.UseSqlServer(DbConnectionFactory.GetSQLConnection(connectionString)));
        }
    }
}
