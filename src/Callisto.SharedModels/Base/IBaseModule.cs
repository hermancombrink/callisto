using Microsoft.EntityFrameworkCore.Storage;

namespace Callisto.SharedModels.Base
{
    /// <summary>
    /// Defines the <see cref="IBaseModule" />
    /// </summary>
    public interface IBaseModule
    {
        /// <summary>
        /// The JoinTransaction
        /// </summary>
        /// <param name="transaction">The <see cref="IDbContextTransaction"/></param>
        void JoinTransaction(IDbContextTransaction transaction);

    }
}
