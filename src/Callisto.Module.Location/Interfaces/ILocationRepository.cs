using Callisto.Module.Locations.Repository.Models;
using System;
using System.Threading.Tasks;

namespace Callisto.Module.Locations.Interfaces
{
    /// <summary>
    /// Defines the <see cref="ILocationRepository" />
    /// </summary>
    public interface ILocationRepository
    {
        /// <summary>
        /// The AddLocation
        /// </summary>
        /// <param name="location">The <see cref="Location"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task AddLocation(Location location);

        /// <summary>
        /// The GetLocationById
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Location}"/></returns>
        Task<Location> GetLocationById(long refId);

        /// <summary>
        /// The GetLocationById
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{Location}"/></returns>
        Task<Location> GetLocationById(Guid id);

        /// <summary>
        /// The GetLocationByPlaceId
        /// </summary>
        /// <param name="placeId">The <see cref="string"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Location}"/></returns>
        Task<Location> GetLocationByPlaceId(string placeId, long companyRefId);

        /// <summary>
        /// The SaveLocation
        /// </summary>
        /// <param name="location">The <see cref="Location"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task SaveLocation(Location location);
    }
}
