using App.Metrics.Health;
using Callisto.Module.Authentication.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Callisto.Module.Authentication.Health
{
    /// <summary>
    /// Defines the <see cref="DbContextHealthCheck" />
    /// </summary>
    public class DbContextHealthCheck : HealthCheck
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DbContextHealthCheck"/> class.
        /// </summary>
        /// <param name="context">The <see cref="ApplicationDbContext"/></param>
        public DbContextHealthCheck() : base("Authentication Db")
        {
        }

        /// <summary>
        /// The CheckAsync
        /// </summary>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/></param>
        /// <returns>The <see cref="ValueTask{HealthCheckResult}"/></returns>
        protected override async ValueTask<HealthCheckResult> CheckAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                //TODO: Figure out a way to inject scoped context into health check
                //option register IDbConnectionFactory... change useSql to work from DbConnection 
                ApplicationDbContext context = default;
                using (var connection = context.Database.GetDbConnection())
                {
                    await connection.OpenAsync();


                    return HealthCheckResult.Healthy();

                };
            }
            catch (Exception ex)
            {
                return HealthCheckResult.Unhealthy($"Connection failed - {ex.Message}");
            }
        }
    }
}
