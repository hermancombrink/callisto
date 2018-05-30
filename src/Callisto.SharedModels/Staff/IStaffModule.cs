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
        Task AddStaffMember();

        /// <summary>
        /// The RemoveStaffMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task RemoveStaffMember();

        /// <summary>
        /// The UpdateStaffMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task UpdateStaffMember();
    }
}
