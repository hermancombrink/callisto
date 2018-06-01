using Callisto.Module.Locations;
using Callisto.Module.Staff.Interfaces;
using Callisto.Module.Staff.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedKernel.Enum;
using Callisto.SharedModels.Person;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Staff;
using Callisto.SharedModels.Staff.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Module.Staff
{
    /// <summary>
    /// Defines the <see cref="StaffModule" />
    /// </summary>
    public class StaffModule : IStaffModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaffModule"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonProvider{StaffMember}"/></param>
        public StaffModule(ICallistoSession session,
            IPersonProvider<StaffMember> personProvider,
            IStaffRepository staffRepo
            )
        {
            PersonProvider = personProvider;
            Session = session;
            StaffRepo = staffRepo;
        }

        /// <summary>
        /// Gets the PersonProvider
        /// </summary>
        public IPersonProvider<StaffMember> PersonProvider { get; }

        /// <summary>
        /// Gets the Session
        /// </summary>
        public ICallistoSession Session { get; }

        /// <summary>
        /// Gets the StaffRepo
        /// </summary>
        public IStaffRepository StaffRepo { get; }

        /// <summary>
        /// The AddStaffMember
        /// </summary>
        /// <param name="model">The <see cref="AddStaffViewModel"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<RequestResult> AddStaffMember(AddStaffViewModel model)
        {
            var person = ModelFactory.CreateStaffMember(model, Session.CurrentCompanyRef);
            using (var tran = await StaffRepo.BeginTransaction())
            {
                if (model.CreateAccount)
                {
                    var createModel = ModelFactory.CreateStaffUser(model, Session.Authentication.GenerateRandomPassword());

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
                        //TODO: send email link for login here
                    }
                }

                await PersonProvider.AddPerson(person);

                tran.Commit();
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The RemoveStaffMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<RequestResult> RemoveStaffMember(Guid Id)
        {
            var staffMember = await PersonProvider.GetPerson(Id);

            if (staffMember == null)
            {
                throw new InvalidOperationException($"Staff member not found");
            }

            await PersonProvider.RemovePerson(staffMember);

            return RequestResult.Success();
        }

        /// <summary>
        /// The UpdateStaffMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public Task<RequestResult> UpdateStaffMember()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The GetStaffMembers
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{StaffViewModel}}}"/></returns>
        public async Task<RequestResult<IEnumerable<StaffViewModel>>> GetStaffMembers()
        {
            var list = new List<StaffViewModel>();
            var members = await PersonProvider.GetPeople(Session.CurrentCompanyRef);

            foreach (var item in members)
            {
                list.Add(ModelFactory.CreateStaffMember(item));
            }

            return RequestResult<IEnumerable<StaffViewModel>>.Success(list);
        }
    }
}
