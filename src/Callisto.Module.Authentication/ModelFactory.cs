using Callisto.Module.Authentication.Repository.Models;
using Callisto.Module.Authentication.ViewModels;

namespace Callisto.Module.Authentication
{
    /// <summary>
    /// Defines the <see cref="ModelFactory" />
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// The CreateUser
        /// </summary>
        /// <param name="model">The <see cref="RegisterViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="ApplicationUser"/></returns>
        public static ApplicationUser CreateUser(RegisterViewModel model, long companyRefId)
        {
            return new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CompanyRefId = companyRefId
            };
        }

        public static Company CreateCompany(RegisterViewModel model)
        {
            return new Company()
            {
                Description = model.CompanyName,
                Name = model.CompanyName
            };
        }
    }
}
