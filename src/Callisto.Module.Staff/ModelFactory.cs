using Callisto.Module.Staff.Repository.Models;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Staff.ViewModels;
using System;

namespace Callisto.Module.Staff
{
    /// <summary>
    /// Defines the <see cref="ModelFactory" />
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// The CreateStaffMember
        /// </summary>
        /// <param name="model">The <see cref="AddStaffViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="StaffMember"/></returns>
        public static StaffMember CreateStaffMember(AddStaffViewModel model, long companyRefId)
        {
            return new StaffMember()
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
        /// The CreateStaffMember
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="StaffMember"/></returns>
        public static StaffMember CreateStaffMember(NewAccountViewModel model, long companyRefId, string email)
        {
            return new StaffMember()
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
        /// The CreateStaffUser
        /// </summary>
        /// <param name="model">The <see cref="AddStaffViewModel"/></param>
        /// <param name="randomPass">The <see cref="string"/></param>
        /// <returns>The <see cref="RegisterViewModel"/></returns>
        public static RegisterViewModel CreateStaffUser(AddStaffViewModel model, string randomPass)
        {
            return new RegisterViewModel()
            {
                Email = model.Email,
                Password = randomPass,
                ConfirmPassword = randomPass,
                Locked = true
            };
        }

        /// <summary>
        /// The CreateStaffMembers
        /// </summary>
        /// <param name="model">The <see cref="StaffMember"/></param>
        /// <returns>The <see cref="StaffViewModel"/></returns>
        public static StaffViewModel CreateStaffMember(StaffMember model)
        {
            return new StaffViewModel()
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
