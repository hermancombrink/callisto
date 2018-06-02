using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Callisto.Session.Provider
{
    /// <summary>
    /// Defines the <see cref="IWebHostBuilderExtensions" />
    /// </summary>
    public static class IWebHostBuilderExtensions
    {
        /// <summary>
        /// The CreateCallistoBuilder
        /// </summary>
        /// <param name="builder">The <see cref="IWebHostBuilder"/></param>
        /// <returns>The <see cref="IWebHostBuilder"/></returns>
        public static IWebHostBuilder CreateCallistoConfiguration(this IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((builderContext, config) =>
            {
                string[] files = { "appsettings.json", "appmessaging.json" };

                IHostingEnvironment env = builderContext.HostingEnvironment;

                foreach (string file in files)
                {
                    string fileName = file.Split('.')[0];
                    config.AddJsonFile(file, optional: true, reloadOnChange: true)
                           .AddJsonFile($"{fileName}.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                }

                config.AddEnvironmentVariables();
            });

            return builder;
        }
    }
}
