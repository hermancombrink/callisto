using Callisto.SharedKernel;
using Callisto.SharedModels.Base;
using Callisto.SharedModels.Location.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Location
{
    /// <summary>
    /// Defines the <see cref="ILocationModule" />
    /// </summary>
    public interface ILocationModule : IBaseModule
    {
        /// <summary>
        /// The UpsertCompanyLocation
        /// </summary>
        /// <param name="viewModel">The <see cref="LocationViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult<long>> UpsertLocation(LocationViewModel viewModel);

        /// <summary>
        /// The GetLocation
        /// </summary>
        /// <param name="locationRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{RequestResult{LocationViewModel}}"/></returns>
        Task<RequestResult<LocationViewModel>> GetLocation(long locationRefId);

        /// <summary>
        /// The GetLocations
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{LocationViewModel}}}"/></returns>
        Task<RequestResult<IEnumerable<LocationViewModel>>> GetLocations();

        /// <summary>
        /// The RemoveLocation
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> RemoveLocation(Guid id);

        /// <summary>
        /// The SaveLocation
        /// </summary>
        /// <param name="model">The <see cref="LocationViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> SaveLocation(LocationViewModel model);
    }
}
