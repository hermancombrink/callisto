using Callisto.Module.Authentication.Options;
using Callisto.Module.Authentication.Repository;
using Callisto.Module.Authentication.Repository.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Callisto.Module.Authentication.Startup
{
    /// <summary>
    /// Defines the <see cref="IServiceCollectionExtensions" />
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// The AddOnDiskSql
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="config">The <see cref="IConfiguration"/></param>
        /// <param name="authOptions">The <see cref="AuthOptions"/></param>
        /// <param name="connectionString">The <see cref="string"/></param>
        public static IServiceCollection WithOnDiskSql(this IServiceCollection services,
            IConfiguration config, AuthOptions authOptions,
            string connectionString = "DefaultConnection")
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(config.GetConnectionString(connectionString)));

            AddDefaults(services, config, authOptions);

            return services;
        }

        /// <summary>
        /// The AddInMemorySql
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="config">The <see cref="IConfiguration"/></param>
        /// <param name="authOptions">The <see cref="AuthOptions"/></param>
        /// <param name="connectionString">The <see cref="string"/></param>
        public static IServiceCollection WithInMemorySql(this IServiceCollection services,
            IConfiguration config, AuthOptions authOptions,
            string databaseName = "InMemoryDatabase")
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName));

            AddDefaults(services, config, authOptions);

            return services;
        }

        public static IServiceCollection WithJwtTokenAuth(this IServiceCollection services,
            IConfiguration config,
            JwtIssuerOptions issuerOptions)
        {
            SecurityKey _signingKey = default;
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).
            AddJwtBearer(configureOptions => ModelFactory.CreateBearerOptions(issuerOptions, out _signingKey));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = issuerOptions.Issuer;
                options.Audience = issuerOptions.Audience;
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            return services;
        }

        /// <summary>
        /// The AddDefaults
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="config">The <see cref="IConfiguration"/></param>
        /// <param name="authOptions">The <see cref="AuthOptions"/></param>
        private static void AddDefaults(this IServiceCollection services,
            IConfiguration config, AuthOptions authOptions)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options => ModelFactory.CreateIdentityOptions(authOptions))
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
