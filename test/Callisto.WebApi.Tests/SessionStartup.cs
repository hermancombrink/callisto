using Callisto.Core.Metrics.Startup;
using Callisto.Session.Provider;
using Callisto.SharedModels.Messaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Callisto.WebApi.Tests
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

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddSingleton(c => Substitute.For<IMessageCoordinator>());
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseCallistoMonitoring();

            app.UseMiddleware<ServiceExceptionMiddleware>();

            app.UseCors("AllowAll");
            app.UseAuthentication();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
