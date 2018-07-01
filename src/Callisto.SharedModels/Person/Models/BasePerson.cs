using Callisto.SharedKernel.Models;

namespace Callisto.SharedModels.Models
{
    /// <summary>
    /// Defines the <see cref="Person" />
    /// </summary>
    public abstract class BasePerson : BaseEfTagModel
    {
        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Deactivated
        /// </summary>
        public bool Deactivated { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the CompanyRefId
        /// </summary>
        public long CompanyRefId { get; set; }
    }
}
