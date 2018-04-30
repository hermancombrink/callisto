using App.Metrics.Health;
using Callisto.Module.Authentication.Health;
using Callisto.Module.Authentication.Interfaces;
using Callisto.Module.Authentication.Options;
using Callisto.Module.Authentication.Repository;
using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedModels.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace Callisto.Module.Authentication.Startup
{
    /// <summary>
    /// Defines the <see cref="IServiceCollectionExtensions" />
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// The UseCallistoAuthentication
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="config">The <see cref="IConfiguration"/></param>
        /// <param name="authOptions">The <see cref="AuthOptions"/></param>
        /// <param name="issuerOptions">The <see cref="JwtIssuerOptions"/></param>
        /// <param name="dbContextFactory">The <see cref="Action{DbContextOptionsBuilder}"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoAuthentication(this IServiceCollection services,
            IConfiguration config,
            AuthOptions authOptions,
            JwtIssuerOptions issuerOptions,
            Action<DbContextOptionsBuilder> dbContextFactory)
        {
            services.AddDbContext<ApplicationDbContext>(dbContextFactory);

            services.AddIdentity<ApplicationUser, IdentityRole>(options => ModelFactory.CreateIdentityOptions(authOptions))
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.TryAddTransient<IAuthenticationModule, AuthenticationModule>();
            services.TryAddTransient<IAuthenticationRepository, AuthenticationRepository>();

            services.AddAuthentication(c =>
            {
                c.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                c.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   ValidIssuer = issuerOptions.Issuer,
                   ValidAudience = issuerOptions.Audience,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtFactory.SecretKey))
               };
           });

            services.TryAddSingleton<IJwtFactory, JwtFactory>();

            //TODO: inject health check back when fixed
            //services.TryAddSingleton<HealthCheck, DbContextHealthCheck>();

            return services;
        }

        /// <summary>
        /// The UseCallistoAuthentication
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/></param>
        /// <param name="config">The <see cref="IConfiguration"/></param>
        /// <param name="authOptions">The <see cref="AuthOptions"/></param>
        /// <param name="issuerOptions">The <see cref="JwtIssuerOptions"/></param>
        /// <param name="connectionString">The <see cref="string"/></param>
        /// <returns>The <see cref="IServiceCollection"/></returns>
        public static IServiceCollection AddCallistoAuthentication(this IServiceCollection services,
            IConfiguration config,
            AuthOptions authOptions,
            JwtIssuerOptions issuerOptions,
            string connectionString)
        {
            return AddCallistoAuthentication(services, config, authOptions, issuerOptions, options => options.UseSqlServer(connectionString));
        }
    }
}
