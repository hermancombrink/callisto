using Callisto.Module.Vendor.Repository.Models;
using Callisto.SharedModels.Person;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Callisto.Module.Vendor.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IVendorRepository" />
    /// </summary>
    public interface IVendorRepository : IPersonRepository<VendorMember>
    {
        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="Task{IDbContextTransaction}"/></returns>
        Task<IDbContextTransaction> BeginTransaction();
    }
}
