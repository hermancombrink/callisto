using Callisto.SharedKernel;
using Callisto.SharedModels.Staff.ViewModels;
using System;
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
    }
}
