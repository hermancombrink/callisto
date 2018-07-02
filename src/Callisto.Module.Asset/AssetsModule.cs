using Callisto.Base.Module;
using Callisto.Module.Assets.Interfaces;
using Callisto.Module.Assets.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedKernel.Enum;
using Callisto.SharedModels.Asset;
using Callisto.SharedModels.Assets.ViewModels;
using Callisto.SharedModels.Location.ViewModels;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Module.Assets
{
    /// <summary>
    /// Defines the <see cref="AssetModule" />
    /// </summary>
    public class AssetsModule : BaseModule, IAssetsModule
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
            IAssetsRepository assetRepo,
            IStorage storage) : base(assetRepo)
        {
            Session = session;
            Logger = logger;
            AssetRepo = assetRepo;
            Storage = storage;
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
        /// Gets the Storage
        /// </summary>
        private IStorage Storage { get; }

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

            Asset parent = await GetAssetParentAsync(model.ParentId);

            var asset = ModelFactory.CreateAsset(model, Session.CurrentCompanyRef, parent);
            await AssetRepo.AddAsset(asset);

            return RequestResult.Success($"{asset.Id}");
        }

        /// <summary>
        /// The GetAssetAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetViewModel}}"/></returns>
        public async Task<RequestResult<AssetInfoViewModel>> GetAssetAsync(Guid id)
        {
            var asset = await AssetRepo.GetAssetById(id);

            var location = await AssetRepo.GetBestAssetLocation(asset.RefId);

            var viewModel = ModelFactory.CreateAssetViewModel(asset, location);

            return RequestResult<AssetInfoViewModel>.Success(viewModel);
        }

        /// <summary>
        /// The GetAssetDetailsAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetDetailViewModel}}"/></returns>
        public async Task<RequestResult<AssetDetailViewModel>> GetAssetDetailsAsync(Guid id)
        {
            var asset = await AssetRepo.GetAssetById(id);

            Asset parent = null;
            if (asset.ParentRefId != null)
            {
                parent = await AssetRepo.GetAssetById(asset.ParentRefId.Value);
            }

            var viewModel = ModelFactory.CreateAssetDetailViewModel(asset, parent);

            viewModel.Location = await GetAssetLocationAsync(asset);

            return RequestResult<AssetDetailViewModel>.Success(viewModel);
        }

        /// <summary>
        /// The GetAssetLocationAsync
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/></param>
        /// <returns>The <see cref="Task{LocationViewModel}"/></returns>
        private async Task<LocationViewModel> GetAssetLocationAsync(Asset asset)
        {
            var location = await AssetRepo.GetAssetLocationByAssetId(asset.RefId);

            if (location != null)
            {
                var locationResult = await Session.Location.GetLocation(location.LocationRefId);
                return locationResult.Result;
            }

            return null;
        }

        /// <summary>
        /// The SaveAssetAsync
        /// </summary>
        /// <param name="model">The <see cref="AssetViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> SaveAssetAsync(AssetDetailViewModel model)
        {
            var asset = await AssetRepo.GetAssetById(model.Id);

            if (asset == null)
            {
                throw new InvalidOperationException($"Unable to find asset");
            }

            using (var tran = AssetRepo.BeginTransaction())
            {
                var parent = await GetAssetParentAsync(model.ParentId);

                ModelFactory.SetSaveAssetState(model, asset, parent);

                await AssetRepo.SaveAssetAsync(asset);

                var locationResult = await Session.Location.UpsertLocation(model.Location);
                if (locationResult.Status != RequestStatus.Success)
                {
                    throw new InvalidOperationException($"Failed to create location");
                }

                await AssetRepo.AddAssetLocation(ModelFactory.CreateAssetLocation(asset, locationResult.Result));

                AssetRepo.CommitTransaction();
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The GetTopLevelAssets
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{AssetTreeViewModel}}}"/></returns>
        public async Task<RequestResult<IEnumerable<AssetTreeViewModel>>> GetAssetTreeAsync(Guid? id = null)
        {
            Asset parent = null;
            if (id != null)
            {
                parent = await AssetRepo.GetAssetById(id.Value);
                if (parent == null)
                {
                    throw new InvalidOperationException($"Failed to find parent asset");
                }
            }

            var assets = await AssetRepo.GetAssetTree(Session.CurrentCompanyRef, parent?.RefId);

            var results = new List<AssetTreeViewModel>();

            foreach (var item in assets)
            {
                results.Add(ModelFactory.CreateAssetViewModel(item));
            }

            return RequestResult<IEnumerable<AssetTreeViewModel>>.Success(results);
        }

        /// <summary>
        /// The GetAssetTreeAllAsync
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{AssetTreeViewModel}}}"/></returns>
        public async Task<RequestResult<IEnumerable<AssetTreeViewModel>>> GetAssetTreeAllAsync()
        {
            var assets = await AssetRepo.GetAssetTreeAll(Session.CurrentCompanyRef);

            var results = new List<AssetTreeViewModel>();

            foreach (var item in assets)
            {
                results.Add(ModelFactory.CreateAssetViewModel(item));
            }

            return RequestResult<IEnumerable<AssetTreeViewModel>>.Success(results);
        }

        /// <summary>
        /// The GetPotentialAssetParentsAsync
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{AssetViewModel}}}"/></returns>
        public async Task<RequestResult<IEnumerable<AssetTreeViewModel>>> GetPotentialAssetParentsAsync(Guid Id)
        {
            var asset = await AssetRepo.GetAssetById(Id);

            if (asset == null)
            {
                throw new InvalidOperationException($"Unable to find asset");
            }

            var assets = await AssetRepo.GetPotentialTreeParents(Session.CurrentCompanyRef, asset.RefId);

            var results = new List<AssetTreeViewModel>();

            foreach (var item in assets)
            {
                results.Add(ModelFactory.CreateAssetViewModel(item));
            }

            return RequestResult<IEnumerable<AssetTreeViewModel>>.Success(results);
        }

        /// <summary>
        /// The UploadAssetPicAsync
        /// </summary>
        /// <param name="file">The <see cref="IFormFile"/></param>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> UploadAssetPicAsync(IFormFile file, Guid id)
        {
            var asset = await AssetRepo.GetAssetById(id);

            if (asset == null)
            {
                throw new InvalidOperationException($"Unable to find asset");
            }

            var company = await Session.Authentication.GetCompanyByRefId(asset.CompanyRefId);

            if (company.Status != RequestStatus.Success)
            {
                throw new InvalidOperationException($"Unable to find company");
            }

            var urlPath = string.Empty;
            using (var stream = file.OpenReadStream())
            {
                var bytes = new byte[stream.Length];
                await stream.ReadAsync(bytes, 0, (int)stream.Length);
                urlPath = await Storage.SaveFile(bytes, $"{asset.Id}", $"{company.Result.Id}");
            }

            asset.PictureUrl = urlPath;

            await AssetRepo.SaveAssetAsync(asset);

            return RequestResult.Success(urlPath);
        }

        /// <summary>
        /// The UpdateParentAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <param name="parentid">The <see cref="Guid?"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> UpdateParentAsync(Guid id, Guid? parentid = null)
        {
            var asset = await AssetRepo.GetAssetById(id);

            if (asset == null)
            {
                throw new InvalidOperationException($"Unable to find asset");
            }

            if (parentid == null)
            {
                asset.ParentRefId = null;
            }
            else
            {
                var parent = await AssetRepo.GetAssetById(parentid.Value);

                if (parent == null)
                {
                    throw new InvalidOperationException($"Unable to find asset");
                }

                asset.ParentRefId = parent.RefId;
            }

            await AssetRepo.SaveAssetAsync(asset);

            return RequestResult.Success();
        }

        /// <summary>
        /// The RemoveAssetAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> RemoveAssetAsync(Guid id)
        {
            var asset = await AssetRepo.GetAssetById(id);

            if (asset == null)
            {
                throw new InvalidOperationException($"Unable to find asset");
            }

            var chilren = await AssetRepo.GetAssetChildren(asset);
            if (chilren > 0)
            {
                return RequestResult.Validation("Cannot delete an asset that is associated as a parent with other assets");
            }

            await AssetRepo.RemoveAssetAsync(asset);

            return RequestResult.Success();
        }

        private async Task<Asset> GetAssetParentAsync(Guid? parentId)
        {
            Asset parent = null;
            if (parentId != null && parentId != Guid.Empty)
            {
                parent = await AssetRepo.GetAssetById(parentId.Value);
                if (parent == null)
                {
                    throw new InvalidOperationException($"Failed to find parent asset");
                }
            }
            return parent;
        }
    }
}
