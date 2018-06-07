using Callisto.SharedKernel;
using Callisto.SharedModels.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Staff.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="StaffMember" />
    /// </summary>
    [Table("Staff", Schema = DbConstants.CallistoSchema)]
    public class StaffMember : BasePerson
    {
  
    }
}
