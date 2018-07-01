using Callisto.SharedModels.Base;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;

namespace Callisto.Base.Module
{
    /// <summary>
    /// Defines the <see cref="BaseModule" />
    /// </summary>
    public abstract class BaseModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseModule"/> class.
        /// </summary>
        /// <param name="repository">The <see cref="BaseRepository"/></param>
        public BaseModule(IBaseRepository repository)
        {
            Repository = repository;
        }

        /// <summary>
        /// Gets the Repository
        /// </summary>
        private IBaseRepository Repository { get; }

        /// <summary>
        /// The JoinTransaction
        /// </summary>
        /// <param name="transaction">The <see cref="DbTransaction"/></param>
        public void JoinTransaction(IDbContextTransaction transaction)
        {
            Repository.JoinTransaction(transaction);
        }
    }
}
