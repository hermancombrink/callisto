using Callisto.Module.Vendor.Interfaces;
using Callisto.Module.Vendor.Repository.Models;
using Callisto.Provider.Person.Repository;
using Callisto.SharedModels.Person;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

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
            Context = context;
        }
 

        /// <summary>
        /// Gets the Context
        /// </summary>
        public VendorDbContext Context { get; }

        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="Task{IDbContextTransaction}"/></returns>
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await Context.Database.BeginTransactionAsync();
        }
    }
}
