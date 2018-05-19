using Callisto.Module.Assets.Repository.Models;
using Callisto.SharedModels.Assets.ViewModels;
using Callisto.SharedModels.Location.ViewModels;
using System;

namespace Callisto.Module.Assets
{
    /// <summary>
    /// Defines the <see cref="ModelFactory" />
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// The CreateAsset
        /// </summary>
        /// <param name="model">The <see cref="AssetAddViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Asset"/></returns>
        public static Asset CreateAsset(AssetAddViewModel model, long companyRefId, Asset parent = null)
        {
            return new Asset()
            {
                CompanyRefId = companyRefId,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Description = model.Description,
                Name = model.Name,
                AssetNumber = model.AssetNumber,
                ParentRefId = parent?.RefId
            };
        }

        /// <summary>
        /// The CreateAssetLocation
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/></param>
        /// <param name="locationRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="AssetLocation"/></returns>
        public static AssetLocation CreateAssetLocation(Asset asset, long locationRefId)
        {
            return new AssetLocation()
            {
                AssetRefId = asset.RefId,
                LocationRefId = locationRefId,
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
        }

        /// <summary>
        /// The SetSaveAssetState
        /// </summary>
        /// <param name="model">The <see cref="AssetViewModel"/></param>
        /// <param name="asset">The <see cref="Asset"/></param>
        public static void SetSaveAssetState(AssetDetailViewModel model, Asset asset)
        {
            asset.AssetNumber = model.AssetNumber;
            asset.Description = model.Description;
            asset.Name = model.Name;
            asset.ModifiedAt = DateTime.Now;
        }

        /// <summary>
        /// The CreateAsset
        /// </summary>
        /// <param name="model">The <see cref="Asset"/></param>
        /// <returns>The <see cref="AssetViewModel"/></returns>
        public static AssetInfoViewModel CreateAssetViewModel(Asset model, LocationViewModel location = null)
        {
            return new AssetInfoViewModel()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                AssetNumber = model.AssetNumber,
                PictureUrl = model.PictureUrl,
                HasLocation = location != null,
                Latitude = location?.Latitude,
                Longitude = location?.Longitude,
                FormattedAddress = location?.FormattedAddress
            };
        }

        /// <summary>
        /// The CreateAssetDetailViewModel
        /// </summary>
        /// <param name="model">The <see cref="Asset"/></param>
        /// <returns>The <see cref="AssetDetailViewModel"/></returns>
        public static AssetDetailViewModel CreateAssetDetailViewModel(Asset model)
        {
            return new AssetDetailViewModel()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                AssetNumber = model.AssetNumber,
                PictureUrl = model.PictureUrl
            };
        }

        /// <summary>
        /// The CreateAsset
        /// </summary>
        /// <param name="model">The <see cref="AssetTreeModel"/></param>
        /// <returns>The <see cref="AssetTreeViewModel"/></returns>
        public static AssetTreeViewModel CreateAssetViewModel(AssetTreeModel model)
        {
            return new AssetTreeViewModel()
            {
                Description = model.Description,
                Name = model.Name,
                AssetNumber = model.AssetNumber,
                Id = model.Id,
                Children = model.Children,
                ParentId = model.ParentId
            };
        }
    }
}
