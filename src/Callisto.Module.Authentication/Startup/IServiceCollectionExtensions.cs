using Callisto.Module.Authentication.Repository;
using Callisto.Module.Authentication.Repository.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Module.Authentication.Startup
{
    public static class IServiceCollectionExtensions
    {
        public static void AddSqlAuthenticationModule(this IServiceCollection services, IConfiguration config, string connectionString = "DefaultConnection")
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString(connectionString)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options => options.User.RequireUniqueEmail = true);
        }
    }
}
