using System;

namespace Callisto.SharedModels.Auth.ViewModels
{
    /// <summary>
    /// Defines the <see cref="UserViewModel" />
    /// </summary>
    public class UserViewModel
    {
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
        /// Gets or sets the Company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the SubscriptionId
        /// </summary>
        public Guid SubscriptionId { get; set; }
    }
}
