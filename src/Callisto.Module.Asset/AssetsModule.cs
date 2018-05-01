using Callisto.Module.Assets.Interfaces;
using Callisto.SharedModels.Asset;
using Callisto.SharedModels.Session;
using Microsoft.Extensions.Logging;

namespace Callisto.Module.Assets
{
    /// <summary>
    /// Defines the <see cref="AssetModule" />
    /// </summary>
    public class AssetsModule : IAssetsModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetsModule"/> class.
        /// </summary>
        /// <param name="session">The <see cref="ICallistoSession"/></param>
        /// <param name="logger">The <see cref="ILogger{AssetsModule}"/></param>
        /// <param name="assetRepo">The <see cref="IAssetsRepository"/></param>
        public AssetsModule(
               ICallistoSession session,
            ILogger<AssetsModule> logger,
            IAssetsRepository assetRepo)
        {
            Session = session;
            Logger = logger;
            AssetRepo = assetRepo;
        }

        /// <summary>
        /// Gets the Session
        /// </summary>
        private ICallistoSession Session { get; }

        /// <summary>
        /// Gets the AssetRepo
        /// </summary>
        private IAssetsRepository AssetRepo { get; }

        /// <summary>
        /// Gets the Logger
        /// </summary>
        private ILogger<AssetsModule> Logger { get; }
    }
}
