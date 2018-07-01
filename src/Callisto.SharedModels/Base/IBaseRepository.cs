using Microsoft.EntityFrameworkCore.Storage;

namespace Callisto.SharedModels.Base
{
    /// <summary>
    /// Defines the <see cref="IBaseRepository" />
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        /// The JoinTransaction
        /// </summary>
        /// <param name="transaction">The <see cref="IDbContextTransaction"/></param>
        void JoinTransaction(IDbContextTransaction transaction);

        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="IDbContextTransaction"/></returns>
        IDbContextTransaction BeginTransaction();
    }
}
