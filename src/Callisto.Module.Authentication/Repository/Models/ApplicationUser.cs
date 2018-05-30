using Microsoft.AspNetCore.Identity;

namespace Callisto.Module.Authentication.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="ApplicationUser" />
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the CompanyName
        /// </summary>
        public long? CompanyRefId { get; set; }

        /// <summary>
        /// Gets or sets the JobRole
        /// </summary>
        public string JobRole { get; set; }
    }
}
