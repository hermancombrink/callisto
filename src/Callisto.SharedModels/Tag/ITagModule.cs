using Callisto.SharedKernel;
using Callisto.SharedModels.Base;
using Callisto.SharedModels.Tag.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Tag
{
    /// <summary>
    /// Defines the <see cref="ITagModule" />
    /// </summary>
    public interface ITagModule : IBaseModule
    {
        /// <summary>
        /// The UpsertCompanyTag
        /// </summary>
        /// <param name="viewModel">The <see cref="TagViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult<long>> UpsertTag(TagViewModel viewModel);

        /// <summary>
        /// The GetTag
        /// </summary>
        /// <param name="TagRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{RequestResult{TagViewModel}}"/></returns>
        Task<RequestResult<TagViewModel>> GetTag(long TagRefId);

        /// <summary>
        /// The GetTags
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{TagViewModel}}}"/></returns>
        Task<RequestResult<IEnumerable<TagViewModel>>> GetTags();

        /// <summary>
        /// The RemoveTag
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> RemoveTag(Guid id);

        /// <summary>
        /// The SaveTag
        /// </summary>
        /// <param name="model">The <see cref="TagViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> SaveTag(TagViewModel model);
    }
}
