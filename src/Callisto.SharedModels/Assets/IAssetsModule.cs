﻿using Callisto.SharedKernel;
using Callisto.SharedModels.Assets.ViewModels;
using System;
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
        /// <returns>The <see cref="Task{RequestResult{AssetViewModel}}"/></returns>
        Task<RequestResult<AssetViewModel>> GetAssetAsync(Guid id);
    }
}
