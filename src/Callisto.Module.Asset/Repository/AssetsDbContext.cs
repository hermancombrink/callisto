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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AssetLocation>().HasKey(table => new {
                table.AssetRefId,
                table.LocationRefId
            });
        }

        /// <summary>
        /// Gets or sets the Assets
        /// </summary>
        public DbSet<Asset> Assets { get; set; }

        /// <summary>
        /// Gets or sets the AssetLocations
        /// </summary>
        public DbSet<AssetLocation> AssetLocations { get; set; }
    }
}
