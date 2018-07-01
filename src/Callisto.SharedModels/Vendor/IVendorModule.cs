using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Base;
using Callisto.SharedModels.Vendor.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Vendor
{
    /// <summary>
    /// Defines the <see cref="IVendorModule" />
    /// </summary>
    public interface IVendorModule : IBaseModule
    {
        /// <summary>
        /// The AddVendorMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> AddVendorMember(AddVendorViewModel model);

        /// <summary>
        /// The RemoveVendorMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> RemoveVendorMember(Guid Id);

        /// <summary>
        /// The UpdateVendorMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        Task<RequestResult> UpdateVendorMember();

        /// <summary>
        /// The GetVendorMembers
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{VendorViewModel}}}"/></returns>
        Task<RequestResult<IEnumerable<VendorViewModel>>> GetVendorMembers();

        /// <summary>
        /// The UpdateCurrentMember
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        Task<RequestResult> UpdateCurrentMember(NewAccountViewModel model);
    }
}
