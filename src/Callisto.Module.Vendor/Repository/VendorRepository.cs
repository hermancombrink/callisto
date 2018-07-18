using Callisto.Module.Vendor.Interfaces;
using Callisto.Module.Vendor.Repository.Models;
using Callisto.Provider.Person.Repository;

namespace Callisto.Module.Vendor.Repository
{
    /// <summary>
    /// Defines the <see cref="VendorRepository" />
    /// </summary>
    public class VendorRepository : PersonRepository<VendorMember>, IVendorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VendorRepository"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonRepository{VendorMember}"/></param>
        public VendorRepository(VendorDbContext context) : base(context)
        {
        }
    }
}
