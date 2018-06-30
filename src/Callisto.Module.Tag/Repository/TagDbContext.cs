using Callisto.Module.Tag.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace Callisto.Module.Tags.Repository
{
    /// <summary>
    /// Defines the <see cref="AssetDbContext" />
    /// </summary>
    public class TagDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagDbContext"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions{TagDbContext}"/></param>
        public TagDbContext(DbContextOptions<TagDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Tags
        /// </summary>
        public DbSet<TagItem> Tags { get; set; }
    }
}
