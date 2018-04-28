using Callisto.Module.Authentication.Options;
using Callisto.Module.Authentication.Repository;
using Callisto.Module.Authentication.Startup;
using Callisto.Session.Provider;
using Callisto.Session.Provider.Startup;
using Callisto.SharedKernel.Extensions;
using Callisto.SharedModels.Notification;
using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Callisto.Module.Authentication.Tests
{
    /// <summary>
    /// Defines the <see cref="TestStartup" />
    /// </summary>
    public class TestStartup : Callisto.Web.Api.Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestStartup"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/></param>
        public TestStartup(IHostingEnvironment env, IConfiguration configuration) : base(configuration)
        {
            env.ApplicationName = "Callisto.Web.Api";

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
    }
}
