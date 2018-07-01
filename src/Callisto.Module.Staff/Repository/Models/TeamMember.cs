using Callisto.SharedKernel;
using Callisto.SharedModels.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Team.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="TeamMember" />
    /// </summary>
    [Table("TeamMembers", Schema = DbConstants.CallistoSchema)]
    public class TeamMember : BasePerson
    {
        /// <summary>
        /// Gets or sets a value indicating whether IsFounder
        /// </summary>
        public bool IsFounder { get; set; }
    }
}
