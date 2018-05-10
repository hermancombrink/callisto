using Callisto.Module.Locations.Interfaces;
using Callisto.Module.Locations.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Callisto.Module.Locations.Repository
{
    /// <summary>
    /// Defines the <see cref="LocationRepository" />
    /// </summary>
    public class LocationRepository : ILocationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationRepository"/> class.
        /// </summary>
        /// <param name="context">The <see cref="AssetDbContext"/></param>
        public LocationRepository(LocationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the Context
        /// </summary>
        private LocationDbContext Context { get; }

        /// <summary>
        /// The AddLocation
        /// </summary>
        /// <param name="location">The <see cref="Location"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task AddLocation(Location location)
        {
            await Context.Locations.AddAsync(location);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The SaveLocation
        /// </summary>
        /// <param name="location">The <see cref="Location"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task SaveLocation(Location location)
        {
            Context.Locations.Attach(location);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The GetLocationById
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{Location}"/></returns>
        public async Task<Location> GetLocationById(Guid id)
        {
            return await Context.Locations.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// The GetLocationByPlaceId
        /// </summary>
        /// <param name="placeId">The <see cref="string"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Location}"/></returns>
        public async Task<Location> GetLocationByPlaceId(string placeId, long companyRefId)
        {
            return await Context.Locations.FirstOrDefaultAsync(c => c.GooglePlaceId == placeId && c.CompanyRefId == companyRefId);
        }

        /// <summary>
        /// The GetLocationById
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Location}"/></returns>
        public async Task<Location> GetLocationById(long refId)
        {
            return await Context.Locations.FindAsync(refId);
        }
    }
}
