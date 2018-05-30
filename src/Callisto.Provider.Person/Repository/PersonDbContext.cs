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
        /// Initializes a new instance of the <see cref="PersonDbContext{T}"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions{PersonDbContext}"/></param>
        public PersonDbContext(DbContextOptions<PersonDbContext<T>> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Peoeple
        /// </summary>
        public DbSet<T> People { get; set; }
    }
}
