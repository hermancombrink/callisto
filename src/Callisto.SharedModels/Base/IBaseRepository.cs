using Microsoft.EntityFrameworkCore.Storage;

namespace Callisto.SharedModels.Base
{
    /// <summary>
    /// Defines the <see cref="IBaseRepository" />
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="IDbContextTransaction"/></returns>
        IDbContextTransaction BeginTransaction();

        /// <summary>
        /// The CommitTransaction
        /// </summary>
        void CommitTransaction();

        /// <summary>
        /// The RollbackTransaction
        /// </summary>
        void RollbackTransaction();
    }
}
