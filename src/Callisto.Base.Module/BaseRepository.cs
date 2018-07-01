using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

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
        public BaseRepository(DbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the Context
        /// </summary>
        private DbContext Context { get; }

        /// <summary>
        /// The JoinTransaction
        /// </summary>
        /// <param name="transaction">The <see cref="DbTransaction"/></param>
        public void JoinTransaction(IDbContextTransaction transaction)
        {
            Context.Database.UseTransaction(transaction.GetDbTransaction());
        }

        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="DbTransaction"/></returns>
        public IDbContextTransaction BeginTransaction()
        {
            return Context.Database.BeginTransaction();
        }
    }
}
