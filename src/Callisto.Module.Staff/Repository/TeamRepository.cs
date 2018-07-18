using Callisto.Module.Team.Interfaces;
using Callisto.Module.Team.Repository.Models;
using Callisto.Provider.Person.Repository;
using Callisto.SharedKernel;
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
        public TeamRepository(TeamDbContext context) : base(context)
        {
        }
  
    }
}
