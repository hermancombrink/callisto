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
                Name = model.Name
            };
        }
    }
}
