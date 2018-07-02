using Callisto.SharedModels.Base;

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
    }
}
