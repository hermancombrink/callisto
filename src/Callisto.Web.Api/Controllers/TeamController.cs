using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Member.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Web.Api.Controllers
{
    /// <summary>
    /// Defines the <see cref="LocationController" />
    /// </summary>
    [Produces("application/json")]
    [Route("Team")]
    [Authorize]
    public class TeamController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="session">The <see cref="ICallistoSession"/></param>
        public TeamController(ICallistoSession session)
        {
            CallistoSession = session;
        }

        /// <summary>
        /// Gets the CallistoSession
        /// </summary>
        public ICallistoSession CallistoSession { get; }

        /// <summary>
        /// The CreateTeamMember
        /// </summary>
        /// <param name="model">The <see cref="AddMemberViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpPost]
        public async Task<RequestResult> CreateTeamMember([FromBody] AddMemberViewModel model)
        {
            return await CallistoSession.Member.AddTeamMember(model);
        }

        /// <summary>
        /// The GetTeamMember
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpGet]
        public async Task<RequestResult<IEnumerable<MemberViewModel>>> GetTeamMember()
        {
            return await CallistoSession.Member.GetTeamMembers();
        }

        /// <summary>
        /// The RemoveTeamMember
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpDelete("{id}")]
        public async Task<RequestResult> RemoveTeamMember(Guid id)
        {
            return await CallistoSession.Member.RemoveTeamMember(id);
        }

        /// <summary>
        /// The UpdateNewProfile
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpPost("profile")]
        public async Task<RequestResult> UpdateNewProfile([FromBody] NewAccountViewModel model)
        {
            return await CallistoSession.Member.UpdateCurrentMember(model);
        }
    }
}
