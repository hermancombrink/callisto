using Callisto.Module.Assets.Repository.Models;
using System.Threading.Tasks;

namespace Callisto.Module.Assets.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IAssetsRepository" />
    /// </summary>
    public interface IAssetsRepository
    {
        /// <summary>
        /// The AddAsset
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task AddAsset(Asset asset);

        /// <summary>
        /// The GetAssetById
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Asset}"/></returns>
        Task<Asset> GetAssetById(long refId);
    }
}
