using Callisto.Module.Tag.Repository.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Module.Tags.Interfaces
{
    /// <summary>
    /// Defines the <see cref="ITagRepository" />
    /// </summary>
    public interface ITagRepository
    {
        /// <summary>
        /// The AddTag
        /// </summary>
        /// <param name="Tag">The <see cref="Tag"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task AddTag(TagItem Tag);

        /// <summary>
        /// The GetTagById
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Tag}"/></returns>
        Task<TagItem> GetTagById(long refId);

        /// <summary>
        /// The GetTagById
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{Tag}"/></returns>
        Task<TagItem> GetTagById(Guid id);

        /// <summary>
        /// The SaveTag
        /// </summary>
        /// <param name="Tag">The <see cref="Tag"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task SaveTag(TagItem Tag);

        /// <summary>
        /// The GetTagsByCompany
        /// </summary>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{IEnumerable{Tag}}"/></returns>
        Task<IEnumerable<TagItem>> GetTagsByCompany(long companyRefId);

        /// <summary>
        /// The GetTagByName
        /// </summary>
        /// <param name="name">The <see cref="string"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{TagItem}"/></returns>
        Task<TagItem> GetTagByName(string name, long companyRefId);

        /// <summary>
        /// The RemoveTag
        /// </summary>
        /// <param name="Tag">The <see cref="Tag"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task RemoveTag(TagItem Tag);
    }
}
