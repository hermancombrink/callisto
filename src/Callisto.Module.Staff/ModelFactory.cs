using Callisto.Module.Staff.Repository.Models;
using Callisto.SharedModels.Staff.ViewModels;
using System;

namespace Callisto.Module.Locations
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
    }
}
