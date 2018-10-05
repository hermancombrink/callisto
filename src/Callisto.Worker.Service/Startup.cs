using System.Data;
using System.Data.SqlClient;
using Callisto.Core.Messaging.Options;
using Callisto.Core.Messaging.Startup;
using Callisto.Core.Metrics.Startup;
using Callisto.Core.Storage.Options;
using Callisto.Core.Storage.Startup;
using Callisto.Module.Assets.Startup;
using Callisto.Module.Authentication.Options;
using Callisto.Module.Authentication.Startup;
using Callisto.Module.Locations.Startup;
using Callisto.Module.Notification.Options;
using Callisto.Module.Notification.Startup;
using Callisto.Session.Provider.Startup;
using Callisto.SharedKernel.Extensions;
using Callisto.SharedKernel.Messaging;
using Callisto.SharedModels.Notification.Models;
using Callisto.Worker.Service.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;

namespace Callisto.Worker.Service
{
    /// <summary>
    /// Defines the <see cref="Startup" />
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the Configuration
        /// </summary>
        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// The ConfigureServices
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IDbConnection>(c => new SqlConnection(Configuration.GetConnectionString("callisto")));

            services.Configure<TemplateOptions>(Configuration.GetSection("templateOptions"));
            services.AddSingleton(ConsumeBinding.SetBinding<NotificationMessage>("Callisto.Notification"));

            services.AddCallistoAuthentication(
                services.ConfigureAndGet<AuthOptions>(Configuration, "authSettings"),
                services.ConfigureAndGet<JwtIssuerOptions>(Configuration, "jwtSettings"));

            services.AddCallistoStorage(services.ConfigureAndGet<StorageOptions>(Configuration, "storage"));
            services.AddCallistoNotification(services.ConfigureAndGet<MailOptions>(Configuration, "mail"));

            services.AddCallistoAssets();
            services.AddCallistoLocations();
            services.AddCallistoMember();
            services.AddCallistoCustomer();
            services.AddCallistoVendor();

            services.AddCallistoMessaging(
               services.ConfigureAndGet<MessageOptions>(Configuration, "rabbitConnection"),
                services.ConfigureAndGet<MessageExchangeConfig>(Configuration, "rabbitTopology"));

            services.AddCallistoWorkerSession();

            services.AddMvc()
                    .AddCallistoMetrics(services, Configuration)
                    .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

            services.AddTransient<IHostedService, NotificationConsumer>();
            services.AddTransient<IViewRenderService, ViewRenderService>();
        }

        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/></param>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/></param>
        public virtual void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            app.UseCallistoMonitoring();
            app.UseCallistoMessaging();

            app.UseMvc();
        }
    }
}
