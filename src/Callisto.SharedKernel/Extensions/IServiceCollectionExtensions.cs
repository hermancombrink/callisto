using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Callisto.SharedKernel.Extensions
{
    public static class IServiceCollectionExtensions
    {
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
