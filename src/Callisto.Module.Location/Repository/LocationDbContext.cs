using Callisto.Module.Locations.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Callisto.Module.Locations.Repository
{
    /// <summary>
    /// Defines the <see cref="AssetDbContext" />
    /// </summary>
    public class LocationDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationDbContext"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions{LocationDbContext}"/></param>
        public LocationDbContext(DbContextOptions<LocationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Locations
        /// </summary>
        public DbSet<Location> Locations { get; set; }
    }
}
