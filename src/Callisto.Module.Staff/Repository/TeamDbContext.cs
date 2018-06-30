using Callisto.Base.Person.Repository;
using Callisto.Module.Team.Repository.Models;
using Callisto.Provider.Person.Repository;
using Microsoft.EntityFrameworkCore;

namespace Callisto.Module.Team.Repository
{
    /// <summary>
    /// Defines the <see cref="TeamDbContext" />
    /// </summary>
    public class TeamDbContext : PersonDbContext<TeamMember>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamDbContext"/> class.
        /// </summary>
        /// <param name="options">The <see cref="DbContextOptions{TeamDbContext}"/></param>
        public TeamDbContext(DbContextOptions<TeamDbContext> options) : base(options)
        {
        }
    }
}
