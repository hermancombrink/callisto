using Callisto.Module.Assets.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Callisto.Module.Assets.Repository
{
    /// <summary>
    /// Defines the <see cref="AssetDbContext" />
    /// </summary>
    public class AssetDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetDbContext"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions{AssetDbContext}"/></param>
        public AssetDbContext(DbContextOptions<AssetDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Assets
        /// </summary>
        public DbSet<Asset> Assets { get; set; }
    }
}
