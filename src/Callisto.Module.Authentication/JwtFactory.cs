using Callisto.Module.Authentication.Interfaces;
using Callisto.Module.Authentication.Options;
using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using IdentityModel;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Callisto.Module.Authentication
{
    /// <summary>
    /// Defines the <see cref="JwtFactory" />
    /// </summary>
    public class JwtFactory : IJwtFactory
    {
        /// <summary>
        /// Defines the _jwtOptions
        /// </summary>
        private readonly JwtIssuerOptions _jwtOptions;

        /// <summary>
        /// Defines the SecretKey
        /// </summary>
        public static string SecretKey = "Wm9mqvNcuJldbmnvuUclG7W45wqiqM0w";

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtFactory"/> class.
        /// </summary>
        /// <param name="jwtOptions">The <see cref="IOptions{JwtIssuerOptions}"/></param>
        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        /// <returns>The <see cref="ClaimsIdentity"/></returns>
        /// <summary>
        /// The GetCalims
        /// </summary>
        /// <param name="userName">The <see cref="string"/></param>
        /// <param name="id">The <see cref="string"/></param>
        /// <returns>The <see cref="Claim[]"/></returns>
        private Claim[] GetCalims(UserViewModel user)
        {
            var claims = new[]
            {
                new Claim(CallistoJwtClaimTypes.Name, user.UserName),
                new Claim(CallistoJwtClaimTypes.Id, user.Id),
                new Claim(CallistoJwtClaimTypes.Company, $"{user.CompanyRefId}"),
                new Claim(CallistoJwtClaimTypes.Subscription, $"{user.SubscriptionRefId}"),
                new Claim(CallistoJwtClaimTypes.Email, $"{user.Email}"),
                new Claim(CallistoJwtClaimTypes.EmailVerified, $"{user.EmailVerified}")
            };

            return claims;
        }

        /// <summary>
        /// The GetTokenAsync
        /// </summary>
        /// <param name="userName">The <see cref="string"/></param>
        /// <param name="Id">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{string}"/></returns>
        public string GetToken(UserViewModel user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityTokenHandler().WriteToken(
                new JwtSecurityToken(
                    issuer: _jwtOptions.Issuer,
                    audience: _jwtOptions.Audience,
                    claims: GetCalims(user),
                    expires: _jwtOptions.Expiration,
                    notBefore: _jwtOptions.NotBefore,
                    signingCredentials: creds));
        }
    }
}
