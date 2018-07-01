using Callisto.Module.Team;
using Callisto.Module.Team.Interfaces;
using Callisto.Module.Team.Repository;
using Callisto.SharedKernel;
using Callisto.SharedModels.Member;
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
        public static IServiceCollection AddCallistoMember(this IServiceCollection services,
        Action<DbContextOptionsBuilder> dbContextFactory)
        {
            services.AddDbContext<TeamDbContext>(dbContextFactory);
            services.TryAddTransient<ITeamRepository, TeamRepository>();
            services.TryAddTransient<IMemberModule, MemberModule>();

            return services;
        }

        /// <summary>
        /// The AddCallistoAssets
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="connectionString">The <see cref="string"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoMember(this IServiceCollection services,
        string connectionString)
        {
            return AddCallistoMember(services, options => options.UseSqlServer(DbConnectionFactory.GetSQLConnection(connectionString)));
        }
    }
}
