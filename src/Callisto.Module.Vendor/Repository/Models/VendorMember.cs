using Callisto.SharedKernel;
using Callisto.SharedModels.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Callisto.Module.Vendor.Repository.Models
{
    /// <summary>
    /// Defines the <see cref="VendorMember" />
    /// </summary>
    [Table("Vendors", Schema = DbConstants.CallistoSchema)]
    public class VendorMember : BasePerson
    {
  
    }
}
