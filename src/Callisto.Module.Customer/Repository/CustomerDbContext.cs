using Callisto.Module.Customer.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Callisto.Module.Customer.Repository
{
    /// <summary>
    /// Defines the <see cref="CustomerDbContext" />
    /// </summary>
    public class CustomerDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerDbContext"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions{CustomerDbContext}"/></param>
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Customer
        /// </summary>
        public DbSet<CustomerMember> Customer { get; set; }
    }
}
