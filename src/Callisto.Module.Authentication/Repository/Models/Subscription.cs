using Callisto.SharedKernel;
using Callisto.SharedKernel.Models;
using Callisto.SharedModels.Auth;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Authentication.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="Subscription" />
    /// </summary>
    [Table("Subscriptions", Schema = DbConstants.CallistoSchema)]
    public class Subscription : BaseEfModel
    {
        /// <summary>
        /// Gets or sets the CompanyRefId
        /// </summary>
        public long CompanyRefId { get; set; }

        /// <summary>
        /// Gets or sets the JobRole
        /// </summary>
        public string JobRole { get; set; }

        /// <summary>
        /// Gets or sets the UserId
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the UserType
        /// </summary>
        public UserType UserType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Deactivated
        /// </summary>
        public bool Deactivated { get; set; }

        /// <summary>
        /// Gets or sets the LastLogin
        /// </summary>
        public DateTime? LastLogin { get; set; }
    }
}
