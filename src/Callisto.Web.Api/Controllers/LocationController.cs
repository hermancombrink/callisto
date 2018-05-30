using Callisto.SharedKernel;
using Callisto.SharedModels.Location.ViewModels;
using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Web.Api.Controllers
{
    /// <summary>
    /// Defines the <see cref="LocationController" />
    /// </summary>
    [Produces("application/json")]
    [Route("location")]
    [Authorize]
    public class LocationController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="session">The <see cref="ICallistoSession"/></param>
        public LocationController(ICallistoSession session)
        {
            CallistoSession = session;
        }

        /// <summary>
        /// Gets the CallistoSession
        /// </summary>
        public ICallistoSession CallistoSession { get; }

        /// <summary>
        /// The GetLocations
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{LocationViewModel}}}"/></returns>
        [HttpGet]
        public async Task<RequestResult<IEnumerable<LocationViewModel>>> GetLocations()
        {
            return await CallistoSession.Location.GetLocations();
        }

        /// <summary>
        /// The RemoveAssetAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpDelete("{id}")]
        public async Task<RequestResult> Remove(Guid id)
        {
            return await CallistoSession.Location.RemoveLocation(id);
        }

        /// <summary>
        /// The SaveLocation
        /// </summary>
        /// <param name="model">The <see cref="LocationViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpPut]
        public async Task<RequestResult> SaveLocation([FromBody] LocationViewModel model)
        {
            return await CallistoSession.Location.SaveLocation(model);
        }
    }
}
