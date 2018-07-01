using Callisto.Base.Module;
using Callisto.Module.Tag.Repository.Models;
using Callisto.Module.Tags.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Callisto.Module.Tags.Repository
{
    /// <summary>
    /// Defines the <see cref="TagRepository" />
    /// </summary>
    public class TagRepository : BaseRepository, ITagRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TagRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="AssetDbContext"/></param>
        public TagRepository(TagDbContext context) : base(context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the Context
        /// </summary>
        private TagDbContext Context { get; }

        /// <summary>
        /// The AddTag
        /// </summary>
        /// <param name="Tag">The <see cref="Tag"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task AddTag(TagItem Tag)
        {
            await Context.Tags.AddAsync(Tag);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The SaveTag
        /// </summary>
        /// <param name="Tag">The <see cref="Tag"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task SaveTag(TagItem Tag)
        {
            Context.Tags.Attach(Tag);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The GetTagById
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{Tag}"/></returns>
        public async Task<TagItem> GetTagById(Guid id)
        {
            return await Context.Tags.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// The GetTagByName
        /// </summary>
        /// <param name="name">The <see cref="string"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{TagItem}"/></returns>
        public async Task<TagItem> GetTagByName(string name, long companyRefId)
        {
            return await Context.Tags.FirstOrDefaultAsync(c => c.Name == name && c.CompanyRefId == companyRefId);
        }

        /// <summary>
        /// The GetTagById
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Tag}"/></returns>
        public async Task<TagItem> GetTagById(long refId)
        {
            return await Context.Tags.FindAsync(refId);
        }

        /// <summary>
        /// The GetTagsByCompany
        /// </summary>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{IEnumerable{Tag}}"/></returns>
        public async Task<IEnumerable<TagItem>> GetTagsByCompany(long companyRefId)
        {
            return await Context.Tags.Where(c => c.CompanyRefId == companyRefId).ToListAsync();
        }

        /// <summary>
        /// The RemoveTag
        /// </summary>
        /// <param name="Tag">The <see cref="Tag"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveTag(TagItem Tag)
        {
            Context.Tags.Remove(Tag);
            await Context.SaveChangesAsync();
        }
    }
}
