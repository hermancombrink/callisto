using Callisto.SharedKernel.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Authentication.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="Subscription" />
    /// </summary>
    [Table("Companies", Schema = "callisto")]
    public class Subscription : BaseEfModel
    {
        /// <summary>
        /// Gets or sets the CompanyRefId
        /// </summary>
        public long CompanyRefId { get; set; }
    }
}
