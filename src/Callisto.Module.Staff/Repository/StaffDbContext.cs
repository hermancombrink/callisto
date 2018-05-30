using Callisto.Module.Staff.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Callisto.Module.Staff.Repository
{
    /// <summary>
    /// Defines the <see cref="StaffDbContext" />
    /// </summary>
    public class StaffDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaffDbContext"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions{StaffDbContext}"/></param>
        public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Staff
        /// </summary>
        public DbSet<StaffMember> Staff { get; set; }
    }
}
