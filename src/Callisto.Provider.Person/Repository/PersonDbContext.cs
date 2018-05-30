using Callisto.SharedModels.Models;
using Microsoft.EntityFrameworkCore;

namespace Callisto.Provider.Person.Repository
{
    /// <summary>
    /// Defines the <see cref="PersonDbContext" />
    /// </summary>
    public class PersonDbContext<T> : DbContext where T : BasePerson
    {
        /// <summary>
        /// Gets or sets the Peoeple
        /// </summary>
        public DbSet<T> People { get; set; }
    }
}
