using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Member.ViewModels;
using Callisto.SharedModels.Person;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Member
{
    /// <summary>
    /// Defines the <see cref="IMemberModule" />
    /// </summary>
    public interface IMemberModule
    {
        /// <summary>
        /// The AddTeamMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> AddTeamMember(AddMemberViewModel model);

        /// <summary>
        /// The RemoveTeamMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> RemoveTeamMember(Guid Id);

        /// <summary>
        /// The UpdateTeamMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> UpdateTeamMember();

        /// <summary>
        /// The GetTeamMembers
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{MemberViewModel}}}"/></returns>
        Task<RequestResult<IEnumerable<MemberViewModel>>> GetTeamMembers();

        /// <summary>
        /// The UpdateCurrentMember
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> UpdateCurrentMember(NewAccountViewModel model);
    }
}
