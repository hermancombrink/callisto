using Callisto.SharedKernel;
using Callisto.SharedModels.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Customer.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="CustomerMember" />
    /// </summary>
    [Table("Customer", Schema = DbConstants.CallistoSchema)]
    public class CustomerMember : BasePerson
    {
  
    }
}
