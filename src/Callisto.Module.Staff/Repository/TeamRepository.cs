using Callisto.Module.Team.Interfaces;
using Callisto.Module.Team.Repository.Models;
using Callisto.Provider.Person.Repository;
using Callisto.SharedModels.Person;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Callisto.Module.Team.Repository
{
    /// <summary>
    /// Defines the <see cref="TeamRepository" />
    /// </summary>
    public class TeamRepository : PersonRepository<TeamMember>, ITeamRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TeamRepository"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonRepository{TeamMember}"/></param>
        public TeamRepository(TeamDbContext context)
            : base(context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the Context
        /// </summary>
        public TeamDbContext Context { get; }

        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="Task{IDbContextTransaction}"/></returns>
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await Context.Database.BeginTransactionAsync();
        }
    }
}
