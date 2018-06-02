using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Callisto.Web.Api
{
    /// <summary>
    /// Defines the <see cref="Program" />
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The Main
        /// </summary>
        /// <param name="args">The <see cref="string[]"/></param>
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        /// <summary>
        /// The BuildWebHost
        /// </summary>
        /// <param name="args">The <see cref="string[]"/></param>
        /// <returns>The <see cref="IWebHost"/></returns>
        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                    .ConfigureAppConfiguration((builderContext, config) =>
                    {
                        string[] files = { "appsettings.json", "appmessaging.json" };

                        IHostingEnvironment env = builderContext.HostingEnvironment;

                        foreach (string file in files)
                        {
                            string fileName = file.Split('.')[0];
                            config.AddJsonFile(file, optional: false, reloadOnChange: true)
                                   .AddJsonFile($"{fileName}.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                        }

                        config.AddEnvironmentVariables();
                    })
                   .UseStartup<Startup>()
                   .Build();
        }
    }
}
