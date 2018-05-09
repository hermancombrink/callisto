using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Callisto.Tests.Startups
{
    /// <summary>
    /// Defines the <see cref="TestStartup" />
    /// </summary>
    public class SessionStartup : Callisto.Web.Api.Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestStartup"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/></param>
        public SessionStartup(IHostingEnvironment env, IConfiguration configuration) : base(configuration)
        {
            env.ApplicationName = "Callisto.Web.Api";
           
        }
    }
}
