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
        public static Asset CreateAsset(AssetAddViewModel model, long companyRefId)
        {
            return new Asset()
            {
                CompanyRefId = companyRefId,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Description = model.Description,
                Name = model.Name,
                AssetNumber = model.AssetNumber
            };
        }

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
        public static AssetViewModel CreateAsset(Asset model)
        {
            return new AssetViewModel()
            {
                Id = model.Id,
                Description = model.Description,
                Name = model.Name,
                AssetNumber = model.AssetNumber
            };
        }
    }
}
