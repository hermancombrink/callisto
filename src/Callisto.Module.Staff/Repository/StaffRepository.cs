using Callisto.Module.Staff.Interfaces;
using Callisto.Module.Staff.Repository.Models;
using Callisto.SharedModels.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Callisto.Module.Staff.Repository
{
    /// <summary>
    /// Defines the <see cref="StaffRepository" />
    /// </summary>
    public class StaffRepository : IStaffRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaffRepository"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonRepository{StaffMember}"/></param>
        public StaffRepository(IPersonRepository<StaffMember> personRepo, StaffDbContext context)
        {
            PersonRepo = personRepo;
            Context = context;
        }

        /// <summary>
        /// Gets the PersonProvider
        /// </summary>
        public IPersonRepository<StaffMember> PersonRepo { get; }

        /// <summary>
        /// Gets the Context
        /// </summary>
        public StaffDbContext Context { get; }

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
