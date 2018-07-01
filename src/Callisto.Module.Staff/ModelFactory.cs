using Callisto.Module.Team.Repository.Models;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Member.ViewModels;
using System;

namespace Callisto.Module.Team
{
    /// <summary>
    /// Defines the <see cref="ModelFactory" />
    /// </summary>
    public static class ModelFactory
    {
        /// <summary>
        /// The CreateTeamMember
        /// </summary>
        /// <param name="model">The <see cref="AddMemberViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="TeamMember"/></returns>
        public static TeamMember CreateTeamMember(AddMemberViewModel model, long companyRefId)
        {
            return new TeamMember()
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


        /// <returns>The <see cref="TeamMember"/></returns>
        public static TeamMember CreateTeamMember(string email, long companyRefId, string userId)
        {
            return new TeamMember()
            {
                CompanyRefId = companyRefId,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
                Id = Guid.NewGuid(),
                Email = email,
                UserId = userId
            };
        }


        /// <summary>
        /// The CreateTeamMember
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="TeamMember"/></returns>
        public static TeamMember CreateTeamMember(NewAccountViewModel model, long companyRefId, string email)
        {
            return new TeamMember()
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
        /// The CreateTeamUser
        /// </summary>
        /// <param name="model">The <see cref="AddMemberViewModel"/></param>
        /// <param name="randomPass">The <see cref="string"/></param>
        /// <returns>The <see cref="RegisterViewModel"/></returns>
        public static RegisterViewModel CreateTeamUser(AddMemberViewModel model, string randomPass)
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
        /// The CreateTeamMembers
        /// </summary>
        /// <param name="model">The <see cref="TeamMember"/></param>
        /// <returns>The <see cref="MemberViewModel"/></returns>
        public static MemberViewModel CreateTeamMember(TeamMember model)
        {
            return new MemberViewModel()
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
