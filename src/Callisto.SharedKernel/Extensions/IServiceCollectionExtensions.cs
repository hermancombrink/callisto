using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Callisto.SharedKernel.Extensions
{
    /// <summary>
    /// Defines the <see cref="IServiceCollectionExtensions" />
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// The ConfigureAndGet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="config">The <see cref="IConfiguration"/></param>
        /// <param name="sectionName">The <see cref="string"/></param>
        /// <returns>The <see cref="T"/></returns>
        public static T ConfigureAndGet<T>(this IServiceCollection services, IConfiguration config, string sectionName) where T : class, new()
        {
            var section = config.GetSection(sectionName);
            services.Configure<T>(section);
            var instance = new T();

            section.Bind(instance);
            return instance;
        }
    }
}
