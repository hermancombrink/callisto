using Callisto.SharedKernel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;

namespace Callisto.Base.Module
{
    /// <summary>
    /// Defines the <see cref="BaseRepository" />
    /// </summary>
    public abstract class BaseRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/></param>
        public BaseRepository(DbContext context, IDbTransactionFactory transactionFactory)
        {
            Context = context;
            TransactionFactory = transactionFactory;
        }

        /// <summary>
        /// Gets the Context
        /// </summary>
        private DbContext Context { get; }

        /// <summary>
        /// Gets the TransactionFactory
        /// </summary>
        public IDbTransactionFactory TransactionFactory { get; }

        /// <summary>
        /// Gets or sets a value indicating whether CreatedTransactions
        /// </summary>
        private bool CreatedTransaction { get; set; }

        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="DbTransaction"/></returns>
        public IDbContextTransaction BeginTransaction()
        {
            var connection = Context.Database.GetDbConnection().ConnectionString;
            var transaction = TransactionFactory.GetTransaction(connection);
            if (transaction != null)
            {
                CreatedTransaction = false;
                Context.Database.UseTransaction(transaction.GetDbTransaction()); //rather join existing transaction than creating new one... 
                //allows for cross module transactions to be used without the knowledge of higher or lower order transactions in code.
                return new FakeTransaction(); //There is already another transaction in progress, avoid returning is incase of disposable using block
            }
            else
            {
                CreatedTransaction = true;
                transaction = Context.Database.BeginTransaction();
                TransactionFactory.AddTransaction(connection, transaction);
                return transaction;
            }
        }

        /// <summary>
        /// The CommitTransaction
        /// </summary>
        public void CommitTransaction()
        {
            if (CreatedTransaction) //only allow party that created the transaction to commit it
            {
                var connection = Context.Database.GetDbConnection().ConnectionString;
                var transaction = TransactionFactory.GetTransaction(connection);
                if (transaction != null)
                {
                    transaction.Commit();
                    TransactionFactory.RemoveTransaction(connection);
                }
                CreatedTransaction = false;
            }
        }

        /// <summary>
        /// The RollbackTransaction
        /// </summary>
        public void RollbackTransaction()
        {
            var connection = Context.Database.GetDbConnection().ConnectionString;
            var transaction = TransactionFactory.GetTransaction(connection);
            if (transaction != null)  //any party can rollback the transaction... rather cause exception than unintended code behavior here
            {
                transaction.Rollback();
                TransactionFactory.RemoveTransaction(connection);
            }
            CreatedTransaction = false;
        }

        /// <summary>
        /// Defines the <see cref="FakeTransaction" />
        /// </summary>
        private class FakeTransaction : IDbContextTransaction
        {
            /// <summary>
            /// Gets the TransactionId
            /// </summary>
            public Guid TransactionId => Guid.Empty;

            /// <summary>
            /// The Commit
            /// </summary>
            public void Commit()
            {
            }

            /// <summary>
            /// The Dispose
            /// </summary>
            public void Dispose()
            {
            }

            /// <summary>
            /// The Rollback
            /// </summary>
            public void Rollback()
            {
            }
        }
    }
}
