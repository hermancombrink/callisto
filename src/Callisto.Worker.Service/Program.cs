using Callisto.Session.Provider;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Callisto.Worker.Service
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
                   .CreateCallistoConfiguration()
                   .UseStartup<Startup>()
                   .UseDefaultServiceProvider(options => options.ValidateScopes = false)
                   .Build();
        }
    }
}
