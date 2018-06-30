using Callisto.Module.Tag.Repository.Models;
using Callisto.SharedModels.Tag.ViewModels;
using System;

namespace Callisto.Module.Tags
{
    /// <summary>
    /// Defines the <see cref="ModelFactory" />
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// The CreateTag
        /// </summary>
        /// <param name="viewModel">The <see cref="TagViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Tag"/></returns>
        public static TagItem CreateTag(TagViewModel viewModel, long companyRefId)
        {
            return new TagItem()
            {
                Name = viewModel.Name,
                CompanyRefId = companyRefId,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Id = Guid.NewGuid(),
            };
        }

        /// <summary>
        /// The UpdateTag
        /// </summary>
        /// <param name="Tag">The <see cref="Tag"/></param>
        /// <param name="viewModel">The <see cref="TagViewModel"/></param>
        public static void UpdateTag(TagItem Tag, TagViewModel viewModel)
        {
            Tag.ModifiedAt = DateTime.Now;
            Tag.Name = viewModel.Name;
        }

        /// <summary>
        /// The CreateTag
        /// </summary>
        /// <param name="Tag">The <see cref="Tag"/></param>
        /// <returns>The <see cref="TagViewModel"/></returns>
        public static TagViewModel CreateTag(TagItem Tag)
        {
            return new TagViewModel()
            {
                Id = Tag.Id,
                Name = Tag.Name
            };
        }
    }
}
