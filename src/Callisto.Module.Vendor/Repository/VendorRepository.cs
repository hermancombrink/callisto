using Callisto.Module.Vendor.Interfaces;
using Callisto.Module.Vendor.Repository.Models;
using Callisto.SharedModels.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Callisto.Module.Vendor.Repository
{
    /// <summary>
    /// Defines the <see cref="VendorRepository" />
    /// </summary>
    public class VendorRepository : IVendorRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VendorRepository"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonRepository{VendorMember}"/></param>
        public VendorRepository(IPersonRepository<VendorMember> personRepo, VendorDbContext context)
        {
            PersonRepo = personRepo;
            Context = context;
        }

        /// <summary>
        /// Gets the PersonProvider
        /// </summary>
        public IPersonRepository<VendorMember> PersonRepo { get; }

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
