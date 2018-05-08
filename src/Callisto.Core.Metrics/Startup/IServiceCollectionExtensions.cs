using App.Metrics;
using App.Metrics.Formatters.Prometheus;
using App.Metrics.Health;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Callisto.Core.Metrics.Startup
{
    /// <summary>
    /// Defines the <see cref="IServiceCollectionExtensions" />
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// The AddCallistoMonitoring
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="config">The <see cref="IConfiguration"/></param>
        public static void AddCallistoMonitoring(this IServiceCollection services,
            IConfiguration config)
        {
            var metrics = AppMetrics.CreateDefaultBuilder()
             .OutputMetrics.AsPrometheusPlainText()
             .Build();
            
            services.AddMetrics();
            services.AddMetricsTrackingMiddleware();
            services.AddMetricsEndpoints(config, c =>
            {
                c.MetricsEndpointOutputFormatter =
                metrics.OutputMetricsFormatters.FirstOrDefault(x => x.GetType() == typeof(MetricsPrometheusTextOutputFormatter));
            });
            services.AddHealthEndpoints(config);
            services.AddHealth(c => c.BuildAndAddTo(services));

            
        }

        /// <summary>
        /// The UseCallistoMonitoring
        /// </summary>
        /// <param name="app">The <see cref="IApplicationBuilder"/></param>
        public static void UseCallistoMonitoring(this IApplicationBuilder app)
        {
            app.UseMetricsAllMiddleware();
            app.UseMetricsAllEndpoints();
            app.UseHealthAllEndpoints();
        }
    }
}
