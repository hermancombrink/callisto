using Callisto.SharedKernel;
using Callisto.SharedModels.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Team.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="TeamMember" />
    /// </summary>
    [Table("Team", Schema = DbConstants.CallistoSchema)]
    public class TeamMember : BasePerson
    {
  
    }
}
