using Callisto.Module.Team.Repository.Models;
using Callisto.SharedModels.Person;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Callisto.Module.Team.Interfaces
{
    /// <summary>
    /// Defines the <see cref="ITeamRepository" />
    /// </summary>
    public interface ITeamRepository : IPersonRepository<TeamMember>
    {
    }
}
