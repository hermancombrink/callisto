using Microsoft.EntityFrameworkCore;
using System.Transactions;

namespace Callisto.Base.Module
{
    /// <summary>
    /// Defines the <see cref="BaseRepository" />
    /// </summary>
    public abstract class BaseRepository<T> where T : DbContext
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private T _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/></param>
        public BaseRepository(T context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the Context
        /// </summary>
        public T Context
        {
            get
            {
                //if the transaction is still active and we have not joined it... implicitly join the transaction.
                if (Transaction.Current != null && Transaction.Current.TransactionInformation.Status == TransactionStatus.Active 
                    && _context.Database.CurrentTransaction == null
                    && _context.Database.GetEnlistedTransaction() == null)
                {
                    _context.Database.OpenConnection();
                    _context.Database.EnlistTransaction(Transaction.Current);
                }

                return _context;
            }
        }
    }
}
