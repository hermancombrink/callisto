using Callisto.SharedKernel;
using Callisto.SharedModels.Assets.ViewModels;
using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Callisto.Web.Api.Controllers
{
    /// <summary>
    /// Defines the <see cref="AssetController" />
    /// </summary>
    [Produces("application/json")]
    [Route("asset")]
    [Authorize]
    public class AssetController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetController"/> class.
        /// </summary>
        /// <param name="session">The <see cref="ICallistoSession"/></param>
        public AssetController(ICallistoSession session)
        {
            CallistoSession = session;
        }

        /// <summary>
        /// Gets the CallistoSession
        /// </summary>
        public ICallistoSession CallistoSession { get; }

        /// <summary>
        /// The LoginAsync
        /// </summary>
        /// <param name="model">The <see cref="LoginViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult{string}}"/></returns>
        [HttpPost("create")]
        public async Task<RequestResult> CreateAssetAsync([FromBody] AssetAddViewModel model)
        {
            return await CallistoSession.Assets.AddAssetAsync(model);
        }
    }
}
