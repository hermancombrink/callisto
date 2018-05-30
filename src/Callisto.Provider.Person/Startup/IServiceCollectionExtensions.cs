using Callisto.Provider.Person;
using Callisto.Provider.Person.Repository;
using Callisto.SharedModels.Models;
using Callisto.SharedModels.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace Callisto.Session.Provider.Startup
{
    /// <summary>
    /// Defines the <see cref="IServiceCollectionExtensions" />
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// The UseCallistoSession
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistPerson<T>(this IServiceCollection services, Action<DbContextOptionsBuilder> dbContextFactory) where T : BasePerson
        {
            services.AddDbContext<PersonDbContext<T>>(dbContextFactory);
            services.TryAddTransient<IPersonRepository<T>, PersonRepository<T>>();
            services.TryAddTransient<IPersonProvider<T>, PersonProvider<T>>();
            return services;
        }
    }
}
