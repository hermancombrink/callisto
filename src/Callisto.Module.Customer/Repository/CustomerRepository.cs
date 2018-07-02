using Callisto.Module.Customer.Interfaces;
using Callisto.Module.Customer.Repository.Models;
using Callisto.Provider.Person.Repository;
using Callisto.SharedKernel;
using Callisto.SharedModels.Person;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Callisto.Module.Customer.Repository
{
    /// <summary>
    /// Defines the <see cref="CustomerRepository" />
    /// </summary>
    public class CustomerRepository : PersonRepository<CustomerMember>, ICustomerRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerRepository"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonRepository{CustomerMember}"/></param>
        public CustomerRepository(CustomerDbContext context, IDbTransactionFactory transactionFactory) : base(context, transactionFactory)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the Context
        /// </summary>
        public CustomerDbContext Context { get; }
    }
}
