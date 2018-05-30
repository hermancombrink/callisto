using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Callisto.Module.Staff.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IStaffRepository" />
    /// </summary>
    public interface IStaffRepository
    {
        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="Task{IDbContextTransaction}"/></returns>
        Task<IDbContextTransaction> BeginTransaction();
    }
}
