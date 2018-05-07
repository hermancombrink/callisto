using Callisto.Module.Assets.Interfaces;
using Callisto.Module.Assets.Repository.Models;
using EntityFrameworkCore.RawSQLExtensions.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
        /// The SaveAssetAsync
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task SaveAssetAsync(Asset asset)
        {
            Context.Assets.Attach(asset);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The GetAssetChildren
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/></param>
        /// <returns>The <see cref="Task{int}"/></returns>
        public async Task<int> GetAssetChildren(Asset asset)
        {
            return await Context.Assets.CountAsync(c => c.ParentRefId == asset.RefId);
        }

        /// <summary>
        /// The RemoveAssetAsync
        /// </summary>
        /// <param name="asset">The <see cref="Asset"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveAssetAsync(Asset asset)
        {
            Context.Assets.Remove(asset);
            await Context.SaveChangesAsync();
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

        /// <summary>
        /// The GetTopLevelAssets
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{IEnumerable{Asset}}"/></returns>
        public async Task<IEnumerable<AssetTreeModel>> GetAssetTree(long companyRefId, long? refId = null)
        {
            return await Context.Database.StoredProcedure<AssetTreeModel>("callisto.usp_GetAssetTree",
                new SqlParameter("@CompanyRefId", companyRefId),
                new SqlParameter("@RefId", refId)).ToListAsync();
        }
    }
}
