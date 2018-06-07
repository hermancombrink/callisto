using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Staff.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Staff
{
    /// <summary>
    /// Defines the <see cref="IStaffModule" />
    /// </summary>
    public interface IStaffModule
    {
        /// <summary>
        /// The AddStaffMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> AddStaffMember(AddStaffViewModel model);

        /// <summary>
        /// The RemoveStaffMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> RemoveStaffMember(Guid Id);

        /// <summary>
        /// The UpdateStaffMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> UpdateStaffMember();

        /// <summary>
        /// The GetStaffMembers
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{StaffViewModel}}}"/></returns>
        Task<RequestResult<IEnumerable<StaffViewModel>>> GetStaffMembers();

        /// <summary>
        /// The UpdateCurrentMember
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> UpdateCurrentMember(NewAccountViewModel model);
    }
}
