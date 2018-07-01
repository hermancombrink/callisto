﻿using Callisto.Module.Locations.Repository.Models;
using Callisto.SharedModels.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Module.Locations.Interfaces
{
    /// <summary>
    /// Defines the <see cref="ILocationRepository" />
    /// </summary>
    public interface ILocationRepository : IBaseRepository
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

        /// <summary>
        /// The GetLocationsByCompany
        /// </summary>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{IEnumerable{Location}}"/></returns>
        Task<IEnumerable<Location>> GetLocationsByCompany(long companyRefId);

        /// <summary>
        /// The RemoveLocation
        /// </summary>
        /// <param name="location">The <see cref="Location"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task RemoveLocation(Location location);
    }
}
