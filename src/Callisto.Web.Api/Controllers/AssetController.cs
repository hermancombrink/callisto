using Callisto.SharedKernel;
using Callisto.SharedModels.Assets.ViewModels;
using Callisto.SharedModels.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

        [HttpPost("pic/{id}")]
        public async Task<RequestResult> UpdatePicutre(IFormFile file, [FromRoute] Guid id)
        {
            return await CallistoSession.Assets.UploadAssetPicAsync(file, id);
        }

        /// <summary>
        /// The SaveAssetAsync
        /// </summary>
        /// <param name="model">The <see cref="AssetViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetViewModel}}"/></returns>
        [HttpPut]
        public async Task<RequestResult<AssetViewModel>> SaveAssetAsync([FromBody] AssetViewModel model)
        {
            return await CallistoSession.Assets.SaveAssetAsync(model);
        }

        /// <summary>
        /// The GetAssetAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetViewModel}}"/></returns>
        [HttpGet("{id}")]
        public async Task<RequestResult<AssetViewModel>> GetAssetAsync(Guid id)
        {
            return await CallistoSession.Assets.GetAssetAsync(id);
        }

        /// <summary>
        /// The GetAssets
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{AssetViewModel}}}"/></returns>
        [HttpGet("tree/{id?}")]
        public async Task<RequestResult<IEnumerable<AssetTreeViewModel>>> GetAssets(Guid? id = null)
        {
            return await CallistoSession.Assets.GetAssetTreeAsync(id);
        }
    }
}
