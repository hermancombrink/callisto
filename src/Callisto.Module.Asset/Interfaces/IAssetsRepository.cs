using Callisto.Module.Assets.Repository.Models;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Module.Assets.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IAssetsRepository" />
    /// </summary>
    public interface IAssetsRepository
    {
        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="Task{IDbContextTransaction}"/></returns>
        Task<IDbContextTransaction> BeginTransaction();

        /// <summary>
        /// The AddAsset
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task AddAsset(Asset asset);

        /// <summary>
        /// The AddAssetLocation
        /// </summary>
        /// <param name="assetLocation">The <see cref="AssetLocation"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task AddAssetLocation(AssetLocation assetLocation);

        /// <summary>
        /// The GetAssetById
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Asset}"/></returns>
        Task<Asset> GetAssetById(long refId);

        /// <summary>
        /// The GetAssetById
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{Asset}"/></returns>
        Task<Asset> GetAssetById(Guid id);

        /// <summary>
        /// The SaveAssetAsync
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task SaveAssetAsync(Asset asset);

        /// <summary>
        /// The GetTopLevelAssets
        /// </summary>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <param name="refiId">The <see cref="long?"/></param>
        /// <returns>The <see cref="Task{IEnumerable{AssetTreeModel}}"/></returns>
        Task<IEnumerable<AssetTreeModel>> GetAssetTree(long companyRefId, long? refiId = null);

        /// <summary>
        /// The GetAssetChildren
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        Task<int> GetAssetChildren(Asset asset);

        /// <summary>
        /// The RemoveAssetAsync
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task RemoveAssetAsync(Asset asset);
    }
}
