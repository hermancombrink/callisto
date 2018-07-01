﻿using Newtonsoft.Json;
using System;

namespace Callisto.SharedModels.Auth.ViewModels
{
    /// <summary>
    /// Defines the <see cref="UserViewModel" />
    /// </summary>
    public class UserViewModel
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        [JsonIgnore]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the CompanyRefId
        /// </summary>
        [JsonIgnore]
        public long CompanyRefId { get; set; }

        /// <summary>
        /// Gets or sets the SubscriptionRefId
        /// </summary>
        [JsonIgnore]
        public long SubscriptionRefId { get; set; }

        /// <summary>
        /// Gets or sets the UserName
        /// </summary>
        [JsonIgnore]
        public string UserName { get; set; }

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
        /// Gets or sets the UserType
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// Gets or sets the Company
        /// </summary>
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets the SubscriptionId
        /// </summary>
        public Guid SubscriptionId { get; set; }

        /// <summary>
        /// Gets a value indicating whether ProfileCompleted
        /// </summary>
        public bool ProfileCompleted => !string.IsNullOrEmpty(FirstName) && !string.IsNullOrEmpty(LastName);

        /// <summary>
        /// Gets a value indicating whether CompanyProfileCompleted
        /// </summary>
        public bool CompanyProfileCompleted => !string.IsNullOrEmpty(Company);

        /// <summary>
        /// Gets or sets a value indicating whether EmailVerified
        /// </summary>
        public bool EmailVerified { get; set; }

        /// <summary>
        /// Gets or sets the JobRole
        /// </summary>
        public string JobRole { get; set; }
    }
}
