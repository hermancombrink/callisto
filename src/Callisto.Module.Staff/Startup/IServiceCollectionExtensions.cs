using Callisto.Module.Team;
using Callisto.Module.Team.Interfaces;
using Callisto.Module.Team.Repository;
using Callisto.SharedKernel;
using Callisto.SharedModels.Member;
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
        public static IServiceCollection AddCallistoMember(this IServiceCollection services)
        {
            services.AddDbContext<TeamDbContext>((service, options) =>
            {
                var connection = service.GetRequiredService<IDbConnection>();
                options.UseSqlServer(connection as DbConnection);
            });
            services.TryAddScoped<ITeamRepository, TeamRepository>();
            services.TryAddScoped<IMemberModule, MemberModule>();

            return services;
        }
    }
}
