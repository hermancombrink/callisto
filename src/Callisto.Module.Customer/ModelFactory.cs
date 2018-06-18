using Callisto.Module.Customer.Repository.Models;
using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Customer.ViewModels;
using System;

namespace Callisto.Module.Customer
{
    /// <summary>
    /// Defines the <see cref="ModelFactory" />
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// The CreateCustomerMember
        /// </summary>
        /// <param name="model">The <see cref="AddCustomerViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="CustomerMember"/></returns>
        public static CustomerMember CreateCustomerMember(AddCustomerViewModel model, long companyRefId)
        {
            return new CustomerMember()
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
        /// The CreateCustomerMember
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="CustomerMember"/></returns>
        public static CustomerMember CreateCustomerMember(NewAccountViewModel model, long companyRefId, string email)
        {
            return new CustomerMember()
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
        /// The CreateCustomerUser
        /// </summary>
        /// <param name="model">The <see cref="AddCustomerViewModel"/></param>
        /// <param name="randomPass">The <see cref="string"/></param>
        /// <returns>The <see cref="RegisterViewModel"/></returns>
        public static RegisterViewModel CreateCustomerUser(AddCustomerViewModel model, string randomPass)
        {
            return new RegisterViewModel()
            {
                Email = model.Email,
                Password = randomPass,
                ConfirmPassword = randomPass,
                Locked = true, 
                UserType = UserType.Customer
            };
        }

        /// <summary>
        /// The CreateCustomerMembers
        /// </summary>
        /// <param name="model">The <see cref="CustomerMember"/></param>
        /// <returns>The <see cref="CustomerViewModel"/></returns>
        public static CustomerViewModel CreateCustomerMember(CustomerMember model)
        {
            return new CustomerViewModel()
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
