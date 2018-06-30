using Callisto.Base.Person.Repository;
using Callisto.Module.Vendor.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Callisto.Module.Vendor.Repository
{
    /// <summary>
    /// Defines the <see cref="VendorDbContext" />
    /// </summary>
    public class VendorDbContext : PersonDbContext<VendorMember>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VendorDbContext"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions{VendorDbContext}"/></param>
        public VendorDbContext(DbContextOptions<VendorDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Vendor
        /// </summary>
        public DbSet<VendorMember> Vendor { get; set; }
    }
}
