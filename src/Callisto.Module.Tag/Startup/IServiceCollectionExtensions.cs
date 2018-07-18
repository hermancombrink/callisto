using Callisto.Module.Tags.Interfaces;
using Callisto.Module.Tags.Repository;
using Callisto.SharedKernel;
using Callisto.SharedModels.Tag;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Callisto.Module.Tags.Startup
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
        public static IServiceCollection AddCallistoTags(this IServiceCollection services,
        Action<DbContextOptionsBuilder> dbContextFactory)
        {
            services.AddDbContext<TagDbContext>(dbContextFactory);
            services.TryAddScoped<ITagRepository, TagRepository>();
            services.TryAddScoped<ITagModule, TagModule>();


            return services;
        }

        /// <summary>
        /// The AddCallistoAssets
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="connectionString">The <see cref="string"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoTags(this IServiceCollection services,
        string connectionString)
        {
            return AddCallistoTags(services, options => options.UseSqlServer(DbConnectionFactory.GetSQLConnection(connectionString)));
        }
    }
}
