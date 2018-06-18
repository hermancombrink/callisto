using Callisto.Module.Customer.Interfaces;
using Callisto.Module.Customer.Repository.Models;
using Callisto.SharedModels.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Callisto.Module.Customer.Repository
{
    /// <summary>
    /// Defines the <see cref="CustomerRepository" />
    /// </summary>
    public class CustomerRepository : ICustomerRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonRepository{CustomerMember}"/></param>
        public CustomerRepository(IPersonRepository<CustomerMember> personRepo, CustomerDbContext context)
        {
            PersonRepo = personRepo;
            Context = context;
        }

        /// <summary>
        /// Gets the PersonProvider
        /// </summary>
        public IPersonRepository<CustomerMember> PersonRepo { get; }

        /// <summary>
        /// Gets the Context
        /// </summary>
        public CustomerDbContext Context { get; }

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
