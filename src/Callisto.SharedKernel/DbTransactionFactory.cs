using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Concurrent;
using System.Data.Common;

namespace Callisto.SharedKernel
{
    /// <summary>
    /// Defines the <see cref="DbTransactionFactory" />
    /// </summary>
    public class DbTransactionFactory : IDbTransactionFactory
    {
        /// <summary>
        /// Defines the CurrentTransactions
        /// </summary>
        private ConcurrentDictionary<string, IDbContextTransaction> CurrentTransactions = new ConcurrentDictionary<string, IDbContextTransaction>();

        /// <summary>
        /// The AddTransaction
        /// </summary>
        /// <param name="connection">The <see cref="string"/></param>
        /// <param name="transaction">The <see cref="IDbContextTransaction"/></param>
        public void AddTransaction(string connection, IDbContextTransaction transaction)
        {
            CurrentTransactions.TryAdd(connection, transaction);
        }

        /// <summary>
        /// The RemoveTransaction
        /// </summary>
        /// <param name="connection">The <see cref="string"/></param>
        public void RemoveTransaction(string connection)
        {
            CurrentTransactions.TryRemove(connection, out _);
        }

        /// <summary>
        /// The GetTransaction
        /// </summary>
        /// <param name="connection">The <see cref="string"/></param>
        /// <returns>The <see cref="IDbContextTransaction"/></returns>
        public IDbContextTransaction GetTransaction(string connection)
        {
            CurrentTransactions.TryGetValue(connection, out IDbContextTransaction transaction);

            try
            {
                if (transaction != null)
                {
                    transaction.GetDbTransaction();
                }

                return transaction;
            }
            catch (ObjectDisposedException)
            {
                return null;
            }
        }
    }
}
