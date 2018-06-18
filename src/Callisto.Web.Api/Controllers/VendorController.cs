using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Vendor.ViewModels;
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
    [Route("vendor")]
    [Authorize]
    public class VendorController : Controller
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationController"/> class.
        /// </summary>
        /// <param name="session">The <see cref="ICallistoSession"/></param>
        public VendorController(ICallistoSession session)
        {
            CallistoSession = session;
        }

        /// <summary>
        /// Gets the CallistoSession
        /// </summary>
        public ICallistoSession CallistoSession { get; }

        /// <summary>
        /// The CreateVendorMember
        /// </summary>
        /// <param name="model">The <see cref="AddVendorViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpPost]
        public async Task<RequestResult> CreateVendorMember([FromBody] AddVendorViewModel model)
        {
            return await CallistoSession.Vendor.AddVendorMember(model);
        }

        /// <summary>
        /// The GetVendorMember
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpGet]
        public async Task<RequestResult<IEnumerable<VendorViewModel>>> GetVendorMember()
        {
            return await CallistoSession.Vendor.GetVendorMembers();
        }

        /// <summary>
        /// The RemoveVendorMember
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpDelete("{id}")]
        public async Task<RequestResult> RemoveVendorMember(Guid id)
        {
            return await CallistoSession.Vendor.RemoveVendorMember(id);
        }

        /// <summary>
        /// The UpdateNewProfile
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        [HttpPost("profile")]
        public async Task<RequestResult> UpdateNewProfile([FromBody] NewAccountViewModel model)
        {
            return await CallistoSession.Vendor.UpdateCurrentMember(model);
        }
    }
}
