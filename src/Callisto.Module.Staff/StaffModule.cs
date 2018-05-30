using Callisto.Module.Locations;
using Callisto.Module.Staff.Interfaces;
using Callisto.Module.Staff.Repository.Models;
using Callisto.SharedModels.Person;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Staff;
using Callisto.SharedModels.Staff.ViewModels;
using System;
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
        public async Task AddStaffMember(AddStaffViewModel model)
        {
            var person = ModelFactory.CreateStaffMember(model, Session.CurrentCompanyRef);
            using (var tran = await StaffRepo.BeginTransaction())
            {
                var createModel = ModelFactory.CreateStaffUser(model, Session.Authentication.GenerateRandomPassword());

                await Session.Authentication.RegisterUserAsync(createModel);

                await PersonProvider.AddPerson(person);

                tran.Commit();
            }
        }

        /// <summary>
        /// The RemoveStaffMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveStaffMember(Guid Id)
        {
            var staffMember = await PersonProvider.GetPerson(Id);

            if (staffMember == null)
            {
                throw new InvalidOperationException($"Staff member not found");
            }

            await PersonProvider.RemovePerson(staffMember);
        }

        /// <summary>
        /// The UpdateStaffMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public Task UpdateStaffMember()
        {
            throw new NotImplementedException();
        }
    }
}
