using Callisto.Base.Module;
using Callisto.Module.Assets.Interfaces;
using Callisto.Module.Assets.Repository.Models;
using Callisto.SharedModels.Location.ViewModels;
using EntityFrameworkCore.RawSQLExtensions.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Callisto.Module.Assets.Repository
{
    /// <summary>
    /// Defines the <see cref="AssetsRepository" />
    /// </summary>
    public class AssetsRepository : BaseRepository, IAssetsRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetsRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="AssetDbContext"/></param>
        public AssetsRepository(AssetDbContext context) : base(context)
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
        /// The UpsertAssetLocation
        /// </summary>
        /// <param name="assetLocation">The <see cref="AssetLocation"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task AddAssetLocation(AssetLocation assetLocation)
        {
            var assetLoc = await Context.AssetLocations.FirstOrDefaultAsync(c => c.AssetRefId == assetLocation.AssetRefId && c.LocationRefId == assetLocation.LocationRefId);
            if (assetLoc == null)
            {
                await Context.AddAsync(assetLocation);
            }
            else
            {
                assetLoc.ModifiedAt = DateTime.Now;
            }
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The GetAssetLocationByAssetId
        /// </summary>
        /// <param name="assetRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{AssetLocation}"/></returns>
        public async Task<AssetLocation> GetAssetLocationByAssetId(long assetRefId)
        {
            return await Context.AssetLocations.OrderByDescending(c => c.LocationRefId).FirstOrDefaultAsync(c => c.AssetRefId == assetRefId);
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

            var locations = await Context.AssetLocations.Where(c => c.AssetRefId == asset.RefId).ToListAsync();
            if (locations != null)
            {
                Context.AssetLocations.RemoveRange(locations);
            }

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

        /// <summary>
        /// The GetAssetTreeAll
        /// </summary>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{IEnumerable{AssetTreeModel}}"/></returns>
        public async Task<IEnumerable<AssetTreeModel>> GetAssetTreeAll(long companyRefId)
        {
            return await Context.Database.StoredProcedure<AssetTreeModel>("callisto.usp_GetAssetTreeAll",
                new SqlParameter("@CompanyRefId", companyRefId)).ToListAsync();
        }

        /// <summary>
        /// The GetPotentialTreeParents
        /// </summary>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{IEnumerable{AssetModel}}"/></returns>
        public async Task<IEnumerable<AssetTreeModel>> GetPotentialTreeParents(long companyRefId, long refId)
        {
            return await Context.Database.StoredProcedure<AssetTreeModel>("callisto.usp_GetPotentialParents",
                 new SqlParameter("@CompanyRefId", companyRefId),
                 new SqlParameter("@RefId", refId)).ToListAsync();
        }

        public async Task<LocationViewModel> GetBestAssetLocation(long refId)
        {
            return await Context.Database.SqlQuery<LocationViewModel>("select * from callisto.udf_GetAssetBestLocation(@RefId)",
                 new SqlParameter("@RefId", refId)).FirstOrDefaultAsync();
        }
    }
}
