using IdentityModel;
using System.Security.Claims;

namespace Callisto.SharedKernel
{
    /// <summary>
    /// Defines the <see cref="CallistoJwtClaimTypes" />
    /// </summary>
    public static class CallistoJwtClaimTypes
    {
        /// <summary>
        /// Defines the Company
        /// </summary>
        public const string Company = "company";

        /// <summary>
        /// Defines the Subscription
        /// </summary>
        public const string Subscription = "subscription";

        /// <summary>
        /// Defines the Email
        /// </summary>
        public const string Email = JwtClaimTypes.Email;

        /// <summary>
        /// Defines the EmailVerified
        /// </summary>
        public const string EmailVerified = JwtClaimTypes.EmailVerified;

        /// <summary>
        /// Defines the Name
        /// </summary>
        public const string Name = ClaimTypes.Name;

        /// <summary>
        /// Defines the Id
        /// </summary>
        public const string Id = JwtClaimTypes.Id;
      
        /// <summary>
        /// Defines the Role
        /// </summary>
        public const string Role = ClaimTypes.Role;
    }
}
