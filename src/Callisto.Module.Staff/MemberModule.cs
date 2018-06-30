using Callisto.Module.Team.Interfaces;
using Callisto.Module.Team.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedKernel.Enum;
using Callisto.SharedKernel.Extensions;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Person;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Member;
using Callisto.SharedModels.Member.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Callisto.Provider.Person;

namespace Callisto.Module.Team
{
    /// <summary>
    /// Defines the <see cref="MemberModule" />
    /// </summary>
    public class MemberModule : PersonModule<TeamMember>, IMemberModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberModule"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonModule{TeamMember}"/></param>
        public MemberModule(ICallistoSession session,
            ITeamRepository teamRepo
            ) : base(teamRepo)
        {
            Session = session;
            TeamRepo = teamRepo;
        }

        /// <summary>
        /// Gets the Session
        /// </summary>
        public ICallistoSession Session { get; }

        /// <summary>
        /// Gets the TeamRepo
        /// </summary>
        public ITeamRepository TeamRepo { get; }

        /// <summary>
        /// The AddTeamMember
        /// </summary>
        /// <param name="model">The <see cref="AddMemberViewModel"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<RequestResult> AddTeamMember(AddMemberViewModel model)
        {
            var person = ModelFactory.CreateTeamMember(model, Session.CurrentCompanyRef);
            using (var tran = await TeamRepo.BeginTransaction())
            {
                if (model.CreateAccount)
                {
                    var createModel = ModelFactory.CreateTeamUser(model, Session.Authentication.GenerateRandomPassword());

                    var result = await Session.Authentication.RegisterUserWithCurrentCompanyAsync(createModel);

                    if (!result.IsSuccess())
                    {
                        return result;
                    }

                    var user = await Session.Authentication.GetUserId(model.Email);
                    if (user.Status != RequestStatus.Success)
                    {
                        throw new InvalidOperationException($"Failed to find user");
                    }

                    person.UserId = user.Result;

                    if (model.SendLink)
                    {
                        var message = Session.Notification.CreateSimpleMessage(NotificationType.AccountInvite, person.Email, result.Result.ToTokenDictionary("~token~"));

                        Session.MessageCoordinator.Publish(message, Session);
                    }
                }

                await AddPerson(person);

                tran.Commit();
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The RemoveTeamMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<RequestResult> RemoveTeamMember(Guid Id)
        {
            var TeamMember = await GetPerson(Id);

            if (TeamMember == null)
            {
                throw new InvalidOperationException($"Team member not found");
            }

            using (var tran = await TeamRepo.BeginTransaction())
            {
                await Session.Authentication.RemoveAccount(TeamMember.Email);

                await RemovePerson(TeamMember);
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The UpdateTeamMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public Task<RequestResult> UpdateTeamMember()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The UpdateCurrentMember
        /// </summary>
        /// <param name="model">The <see cref="NewAccountViewModel"/></param>
        /// <returns>The <see cref="Task{RequestResult}"/></returns>
        public async Task<RequestResult> UpdateCurrentMember(NewAccountViewModel model)
        {
            var user = await Session.Authentication.GetUserId(Session.EmailAddress);
            if (user.Status != RequestStatus.Success)
            {
                throw new InvalidOperationException($"Failed to find user");
            }

            using (var tran = await TeamRepo.BeginTransaction())
            {
                var result = await Session.Authentication.UpdateNewProfileAsync(model);
                if (result.Status == RequestStatus.Success)
                {

                    var member = await GetPersonByUserId(user.Result);
                    if (member == null)
                    {
                        member = ModelFactory.CreateTeamMember(model, Session.CurrentCompanyRef, Session.EmailAddress);

                        await AddPerson(member);
                    }
                    else
                    {

                        member.FirstName = model.FirstName;
                        member.LastName = model.LastName;
                        member.ModifiedAt = DateTime.Now;

                        await UpdatePerson(member);
                    }

                    tran.Commit();
                }
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The GetTeamMembers
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{MemberViewModel}}}"/></returns>
        public async Task<RequestResult<IEnumerable<MemberViewModel>>> GetTeamMembers()
        {
            var list = new List<MemberViewModel>();
            var members = await GetPeople(Session.CurrentCompanyRef);

            foreach (var item in members)
            {
                list.Add(ModelFactory.CreateTeamMember(item));
            }

            return RequestResult<IEnumerable<MemberViewModel>>.Success(list);
        }
    }
}
