using Callisto.Module.Authentication.Options;
using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedModels.Auth.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Callisto.Module.Authentication
{
    /// <summary>
    /// Defines the <see cref="ModelFactory" />
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// The CreateUser
        /// </summary>
        /// <param name="model">The <see cref="RegisterViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="ApplicationUser"/></returns>
        public static ApplicationUser CreateUser(RegisterViewModel model, long companyRefId)
        {
            return new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = string.Empty,
                LastName = string.Empty,
                CompanyRefId = companyRefId
            };
        }

        /// <summary>
        /// The CreateCompany
        /// </summary>
        /// <param name="model">The <see cref="RegisterViewModel"/></param>
        /// <returns>The <see cref="Company"/></returns>
        public static Company CreateCompany(RegisterViewModel model)
        {
            return new Company()
            {
                Id = Guid.NewGuid(),
                Description = string.Empty,
                Name = string.Empty,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }

        /// <summary>
        /// The CreateCompany
        /// </summary>
        /// <param name="company">The <see cref="Company"/></param>
        /// <returns>The <see cref="CompanyViewModel"/></returns>
        public static CompanyViewModel CreateCompany(Company company)
        {
            return new CompanyViewModel()
            {
                Id = company.Id,
                Description = company.Description,
                Name = company.Name
            };
        }

        /// <summary>
        /// The GetSubscription
        /// </summary>
        /// <param name="company">The <see cref="Company"/></param>
        /// <returns>The <see cref="Subscription"/></returns>
        public static Subscription CreateSubscription(Company company)
        {
            return new Subscription()
            {
                Id = Guid.NewGuid(),
                CompanyRefId = company.RefId,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }

        /// <summary>
        /// The CreateIdentityOptions
        /// </summary>
        /// <param name="authOptions">The <see cref="AuthOptions"/></param>
        /// <returns>The <see cref="IdentityOptions"/></returns>
        public static IdentityOptions CreateIdentityOptions(AuthOptions authOptions)
        {
            return new IdentityOptions()
            {
                Password = new PasswordOptions()
                {
                    RequireDigit = authOptions.RequireDigit,
                    RequiredLength = authOptions.RequiredLength,
                    RequireLowercase = authOptions.RequireLowercase,
                    RequireNonAlphanumeric = authOptions.RequireNonAlphanumeric,
                    RequireUppercase = authOptions.RequireUppercase
                },
                User = new UserOptions()
                {
                    RequireUniqueEmail = true
                }
            };
        }

        /// <summary>
        /// The CreateBearerOptions
        /// </summary>
        /// <param name="issuerOptions">The <see cref="JwtIssuerOptions"/></param>
        /// <param name="key">The <see cref="SecurityKey"/></param>
        /// <returns>The <see cref="JwtBearerOptions"/></returns>
        public static JwtBearerOptions CreateBearerOptions(JwtIssuerOptions issuerOptions, SecurityKey key)
        {
            return new JwtBearerOptions()
            {
                ClaimsIssuer = issuerOptions.Issuer,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = issuerOptions.Issuer,
                    ValidAudience = issuerOptions.Audience,
                    IssuerSigningKey = key,
                    RequireExpirationTime = false,
                    ClockSkew = TimeSpan.Zero
                },
                SaveToken = true
            };
        }

        /// <summary>
        /// The UpdateNewUserDetails
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <param name="company">The <see cref="Company"/></param>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        public static void UpdateNewUserDetails(ApplicationUser user, Company company, NewAccountViewModel model)
        {
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            company.Name = model.CompanyName;
        }

        /// <summary>
        /// The CreateRegistration
        /// </summary>
        /// <param name="model">The <see cref="SocialLoginViewModel"/></param>
        /// <returns>The <see cref="RegisterViewModel"/></returns>
        public static RegisterViewModel CreateRegistration(SocialLoginViewModel model)
        {
            var pass = $"dkv0HaHBKPn1wvO1M6rg{Guid.NewGuid()}";
            return new RegisterViewModel()
            {
                Email = model.Email,
                Password = pass,
                ConfirmPassword = pass
            };
        }
    }
}
