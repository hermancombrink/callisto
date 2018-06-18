using Callisto.Module.Customer.Repository.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Callisto.Module.Customer.Interfaces
{
    /// <summary>
    /// Defines the <see cref="ICustomerRepository" />
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="Task{IDbContextTransaction}"/></returns>
        Task<IDbContextTransaction> BeginTransaction();
    }
}
