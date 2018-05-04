using Callisto.Module.Assets.Interfaces;
using Callisto.Module.Assets.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Callisto.Module.Assets.Repository
{
    /// <summary>
    /// Defines the <see cref="AssetsRepository" />
    /// </summary>
    public class AssetsRepository : IAssetsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetsRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="AssetDbContext"/></param>
        public AssetsRepository(AssetDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the Context
        /// </summary>
        private AssetDbContext Context { get; }

        /// <summary>
        /// The AddTask
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task AddAsset(Asset asset)
        {
            await Context.Assets.AddAsync(asset);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The GetAssetById
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Asset}"/></returns>
        public async Task<Asset> GetAssetById(long refId)
        {
            return await Context.Assets.FirstOrDefaultAsync(c => c.RefId == refId);
        }

        /// <summary>
        /// The GetAssetById
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{Asset}"/></returns>
        public async Task<Asset> GetAssetById(Guid id)
        {
            return await Context.Assets.FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
