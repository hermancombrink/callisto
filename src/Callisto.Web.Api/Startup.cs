using Callisto.Module.Authentication.Options;
using Callisto.Module.Authentication.Startup;
using Callisto.Session.Provider;
using Callisto.Session.Provider.Startup;
using Callisto.SharedKernel.Extensions;
using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using System;
using static Callisto.SharedKernel.Extensions.IServiceCollectionExtensions;

namespace Callisto.Web.Api
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

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// The ConfigureServices
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        public virtual void ConfigureServices(IServiceCollection services)
        {
            var connection = Configuration.GetConnectionString("callisto");
            services.UseCallistoAuthentication(
                Configuration,
                services.ConfigureAndGet<AuthOptions>(Configuration, "authSettings"),
                services.ConfigureAndGet<JwtIssuerOptions>(Configuration, "jwtSettings"),
                connection
              //dbFactory =>
              //{
              //    dbFactory.UseInMemoryDatabase("InMemoryDatabase");
              //    dbFactory.ConfigureWarnings(w => w.Ignore(InMemoryEventId.TransactionIgnoredWarning));
              //}
              );

            services.UseCallistoSession();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder
                        .SetPreflightMaxAge(TimeSpan.MaxValue)
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// The Configure
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/></param>
        /// <param name="env">The <see cref="IHostingEnvironment"/></param>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/></param>
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ServiceExceptionMiddleware>();

            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
