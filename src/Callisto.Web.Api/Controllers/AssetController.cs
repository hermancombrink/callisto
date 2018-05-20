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

        /// <summary>
        /// The UpdatePicutre
        /// </summary>
        /// <param name="file">The <see cref="IFormFile"/></param>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
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
        public async Task<RequestResult> SaveAssetAsync([FromBody] AssetDetailViewModel model)
        {
            return await CallistoSession.Assets.SaveAssetAsync(model);
        }

        /// <summary>
        /// The SaveAssetAsync
        /// </summary>
        /// <param name="model">The <see cref="AssetViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetViewModel}}"/></returns>
        [HttpPut("parent/{id}/{parentid?}")]
        public async Task<RequestResult> UpdateParentAsync(Guid id, Guid? parentid = null)
        {
            return await CallistoSession.Assets.UpdateParentAsync(id, parentid);
        }

        /// <summary>
        /// The GetAssetAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetViewModel}}"/></returns>
        [HttpGet("{id}")]
        public async Task<RequestResult<AssetInfoViewModel>> GetAssetAsync(Guid id)
        {
            return await CallistoSession.Assets.GetAssetAsync(id);
        }

        /// <summary>
        /// The GetAssetDetailAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetDetailViewModel}}"/></returns>
        [HttpGet("details/{id}")]
        public async Task<RequestResult<AssetDetailViewModel>> GetAssetDetailAsync(Guid id)
        {
            return await CallistoSession.Assets.GetAssetDetailsAsync(id);
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

        /// <summary>
        /// The GetAssets
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{AssetViewModel}}}"/></returns>
        [HttpGet("tree/all")]
        public async Task<RequestResult<IEnumerable<AssetTreeViewModel>>> GetAssetsAll()
        {
            return await CallistoSession.Assets.GetAssetTreeAllAsync();
        }

        /// <summary>
        /// The GetAssets
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{AssetViewModel}}}"/></returns>
        [HttpGet("tree/parents/{id}")]
        public async Task<RequestResult<IEnumerable<AssetTreeViewModel>>> GetPotentialAssetTreeParents(Guid id)
        {
            return await CallistoSession.Assets.GetPotentialAssetParentsAsync(id);
        }

        /// <summary>
        /// The RemoveAssetAsync
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult{AssetViewModel}}"/></returns>
        [HttpDelete("{id}")]
        public async Task<RequestResult> RemoveAssetAsync(Guid id)
        {
            return await CallistoSession.Assets.RemoveAssetAsync(id);
        }
    }
}
