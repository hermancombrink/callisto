using System.ComponentModel.DataAnnotations;

namespace Callisto.SharedModels.Auth.ViewModels
{
    /// <summary>
    /// Defines the <see cref="NewAccountViewModel" />
    /// </summary>
    public class NewAccountViewModel
    {
        /// <summary>
        /// Gets or sets the FirstName
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the UserRole
        /// </summary>
        public string UserRole { get; set; }

        /// <summary>
        /// Gets or sets the CompanyDetails
        /// </summary>
        public NewCompanyViewModel CompanyDetails { get; set; }
    }

    /// <summary>
    /// Defines the <see cref="NewCompanyViewModel" />
    /// </summary>
    public class NewCompanyViewModel
    {
        /// <summary>
        /// Gets or sets the Company
        /// </summary>
        [Required]
        public string CompanyName { get; set; }

        /// <summary>
        /// Gets or sets the CompanyWebsite
        /// </summary>
        public string CompanyWebsite { get; set; }

        /// <summary>
        /// Gets or sets the CompanySize
        /// </summary>
        public string CompanySize { get; set; }

 
    }
}
