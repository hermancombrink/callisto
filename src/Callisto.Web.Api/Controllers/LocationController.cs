using Callisto.SharedKernel;
using Callisto.SharedModels.Location.ViewModels;
using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
