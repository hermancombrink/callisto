using Microsoft.EntityFrameworkCore.Storage;

namespace Callisto.SharedKernel
{
    /// <summary>
    /// Defines the <see cref="IDbTransactionFactory" />
    /// </summary>
    public interface IDbTransactionFactory
    {
        /// <summary>
        /// The AddTransaction
        /// </summary>
        /// <param name="connection">The <see cref="string"/></param>
        /// <param name="transaction">The <see cref="IDbContextTransaction"/></param>
        void AddTransaction(string connection, IDbContextTransaction transaction);

        /// <summary>
        /// The RemoveTransaction
        /// </summary>
        /// <param name="connection">The <see cref="string"/></param>
        void RemoveTransaction(string connection);

        /// <summary>
        /// The GetTransaction
        /// </summary>
        /// <param name="connection">The <see cref="string"/></param>
        /// <returns>The <see cref="IDbContextTransaction"/></returns>
        IDbContextTransaction GetTransaction(string connection);
    }
}
