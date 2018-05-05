using Callisto.Module.Assets.Interfaces;
using Callisto.Module.Assets.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedModels.Asset;
using Callisto.SharedModels.Assets.ViewModels;
using Callisto.SharedModels.Session;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        /// <summary>
        /// The AddAsset
        /// </summary>
        /// <param name="model">The <see cref="AssetAddViewModel"/></param>
        /// <returns>The <see cref="RequestResult"/></returns>
        public async Task<RequestResult> AddAssetAsync(AssetAddViewModel model)
        {
            if (Session.CurrentCompanyRef == 0)
            {
                throw new ArgumentException($"Session does not contain valid company");
            }

            Asset parent = null;
            if (model.ParentId != default)
            {
                parent = await AssetRepo.GetAssetById(model.ParentId);
                if (parent == null)
                {
                    throw new InvalidOperationException($"Unable to find parent asset");
                }
            }

            var asset = ModelFactory.CreateAsset(model, Session.CurrentCompanyRef, parent);
            await AssetRepo.AddAsset(asset);

            return RequestResult.Success($"{asset.Id}");
        }

        /// <summary>
        /// The GetAssetAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetViewModel}}"/></returns>
        public async Task<RequestResult<AssetViewModel>> GetAssetAsync(Guid id)
        {
            var asset = await AssetRepo.GetAssetById(id);

            var viewModel = ModelFactory.CreateAsset(asset);

            return RequestResult<AssetViewModel>.Success(viewModel);
        }

        /// <summary>
        /// The SaveAssetAsync
        /// </summary>
        /// <param name="model">The <see cref="AssetViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult<AssetViewModel>> SaveAssetAsync(AssetViewModel model)
        {
            var asset = await AssetRepo.GetAssetById(model.Id);

            if (asset == null)
            {
                return RequestResult<AssetViewModel>.Failed($"Failed to find asset {model.Name}", model);
            }

            ModelFactory.SetSaveAssetState(model, asset);

            await AssetRepo.SaveAssetAsync(asset);

            return RequestResult<AssetViewModel>.Success(model);
        }

        /// <summary>
        /// The GetTopLevelAssets
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{AssetViewModel}}}"/></returns>
        public async Task<RequestResult<IEnumerable<AssetViewModel>>> GetTopLevelAssets()
        {
            var assets = await AssetRepo.GetTopLevelAssets(Session.CurrentCompanyRef);

            var results = new List<AssetViewModel>();

            foreach (var item in assets)
            {
                results.Add(ModelFactory.CreateAsset(item));
            }

            return RequestResult<IEnumerable<AssetViewModel>>.Success(results);
        }
    }
}
