using Callisto.SharedKernel;
using Callisto.SharedModels.Assets.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Asset
{
    /// <summary>
    /// Defines the <see cref="IAssetsModule" />
    /// </summary>
    public interface IAssetsModule
    {
        /// <summary>
        /// The AddAssetAsync
        /// </summary>
        /// <param name="model">The <see cref="AssetAddViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> AddAssetAsync(AssetAddViewModel model);

        /// <summary>
        /// The GetAssetAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetInfoViewModel}}"/></returns>
        Task<RequestResult<AssetInfoViewModel>> GetAssetAsync(Guid id);

        /// <summary>
        /// The GetAssetDetailsAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetDetailViewModel}}"/></returns>
        Task<RequestResult<AssetDetailViewModel>> GetAssetDetailsAsync(Guid id);

        /// <summary>
        /// The SaveAssetAsync
        /// </summary>
        /// <param name="model">The <see cref="AssetViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetViewModel}}"/></returns>
        Task<RequestResult> SaveAssetAsync(AssetDetailViewModel model);

        /// <summary>
        /// The GetAssetTree
        /// </summary>
        /// <param name="id">The <see cref="Guid?"/></param>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{AssetTreeViewModel}}}"/></returns>
        Task<RequestResult<IEnumerable<AssetTreeViewModel>>> GetAssetTreeAsync(Guid? id = null);

        /// <summary>
        /// The GetAssetTreeAllAsync
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{AssetTreeViewModel}}}"/></returns>
        Task<RequestResult<IEnumerable<AssetTreeViewModel>>> GetAssetTreeAllAsync();

        /// <summary>
        /// The UploadAssetPicAsync
        /// </summary>
        /// <param name="file">The <see cref="IFormFile"/></param>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> UploadAssetPicAsync(IFormFile file, Guid id);

        /// <summary>
        /// The UpdateParentAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <param name="parentid">The <see cref="Guid?"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> UpdateParentAsync(Guid id, Guid? parentid = null);

        /// <summary>
        /// The RemoveAssetAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> RemoveAssetAsync(Guid id);
    }
}
