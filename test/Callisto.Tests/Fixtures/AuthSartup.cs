using Callisto.Module.Authentication.Options;
using Callisto.Module.Authentication.Startup;
using Callisto.Session.Provider.Startup;
using Callisto.SharedKernel.Extensions;
using Callisto.SharedModels.Notification;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Callisto.Tests.Startups
{
    /// <summary>
    /// Defines the <see cref="TestStartup" />
    /// </summary>
    public class AuthSartup : Callisto.Web.Api.Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestStartup"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/></param>
        public AuthSartup(IHostingEnvironment env, IConfiguration configuration) : base(configuration)
        {
            env.ApplicationName = "Callisto.Web.Api";
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// The ConfigureServices
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddCallistoAuthentication(
                services.ConfigureAndGet<AuthOptions>(Configuration, "authSettings"),
                services.ConfigureAndGet<JwtIssuerOptions>(Configuration, "jwtSettings"),
               dbFactory =>
               {
                   dbFactory.UseInMemoryDatabase("InMemoryDatabase");
                   dbFactory.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
               }
              );

            services.AddCallistoSession();

            services.AddSingleton(c => Substitute.For<INotificationModule>());

            services.AddMvc();
        }

        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/></param>
        /// <param name="env">The <see cref="IHostingEnvironment"/></param>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/></param>
        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
