using Callisto.Module.Staff;
using Callisto.Module.Staff.Interfaces;
using Callisto.Module.Staff.Repository;
using Callisto.Module.Staff.Repository.Models;
using Callisto.Session.Provider.Startup;
using Callisto.SharedModels.Staff;
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
        public static IServiceCollection AddCallistoStaff(this IServiceCollection services,
        Action<DbContextOptionsBuilder> dbContextFactory)
        {
            services.AddCallistPerson<StaffMember>(dbContextFactory);
            services.AddDbContext<StaffDbContext>(dbContextFactory);
            services.TryAddTransient<IStaffRepository, StaffRepository>();
            services.TryAddTransient<IStaffModule, StaffModule>();

            return services;
        }

        /// <summary>
        /// The AddCallistoAssets
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="connectionString">The <see cref="string"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoStaff(this IServiceCollection services,
        string connectionString)
        {
            return AddCallistoStaff(services, options => options.UseSqlServer(connectionString));
        }
    }
}
