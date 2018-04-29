using Microsoft.IdentityModel.Tokens;
using System;

namespace Callisto.Module.Authentication.Options
{
    /// <summary>
    /// Defines the <see cref="JwtIssuerOptions" />
    /// </summary>
    public class JwtIssuerOptions
    {
        /// <summary>
        /// Gets or sets the Issuer
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// Gets or sets the Audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Gets the Expiration
        /// </summary>
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        /// <summary>
        /// Gets the NotBefore
        /// </summary>
        public DateTime NotBefore => DateTime.UtcNow;

        /// <summary>
        /// Gets the IssuedAt
        /// </summary>
        public DateTime IssuedAt => DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the ValidFor
        /// </summary>
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);
    }
}
