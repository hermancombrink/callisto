using Callisto.Module.Vendor.Repository.Models;
using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Vendor.ViewModels;
using System;

namespace Callisto.Module.Vendor
{
    /// <summary>
    /// Defines the <see cref="ModelFactory" />
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// The CreateVendorMember
        /// </summary>
        /// <param name="model">The <see cref="AddVendorViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="VendorMember"/></returns>
        public static VendorMember CreateVendorMember(AddVendorViewModel model, long companyRefId)
        {
            return new VendorMember()
            {
                CompanyRefId = companyRefId,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        /// <summary>
        /// The CreateVendorMember
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="VendorMember"/></returns>
        public static VendorMember CreateVendorMember(NewAccountViewModel model, long companyRefId, string email)
        {
            return new VendorMember()
            {
                CompanyRefId = companyRefId,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Email = email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };
        }

        /// <summary>
        /// The CreateVendorUser
        /// </summary>
        /// <param name="model">The <see cref="AddVendorViewModel"/></param>
        /// <param name="randomPass">The <see cref="string"/></param>
        /// <returns>The <see cref="RegisterViewModel"/></returns>
        public static RegisterViewModel CreateVendorUser(AddVendorViewModel model, string randomPass)
        {
            return new RegisterViewModel()
            {
                Email = model.Email,
                Password = randomPass,
                ConfirmPassword = randomPass,
                Locked = true,
                UserType = UserType.Vendor
            };
        }

        /// <summary>
        /// The CreateVendorMembers
        /// </summary>
        /// <param name="model">The <see cref="VendorMember"/></param>
        /// <returns>The <see cref="VendorViewModel"/></returns>
        public static VendorViewModel CreateVendorMember(VendorMember model)
        {
            return new VendorViewModel()
            {
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Id = model.Id,
                ParentId = null,
                PictureUrl = string.Empty
            };
        }
    }
}
