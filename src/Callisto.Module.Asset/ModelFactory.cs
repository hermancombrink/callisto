using Callisto.Module.Assets.Repository.Models;
using Callisto.SharedModels.Assets.ViewModels;
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
        /// The SetSaveAssetState
        /// </summary>
        /// <param name="model">The <see cref="AssetViewModel"/></param>
        /// <param name="asset">The <see cref="Asset"/></param>
        public static void SetSaveAssetState(AssetViewModel model, Asset asset)
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
        public static AssetViewModel CreateAssetViewModel(Asset model)
        {
            return new AssetViewModel()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                AssetNumber = model.AssetNumber
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
                Children = model.Children
            };
        }
    }
}
