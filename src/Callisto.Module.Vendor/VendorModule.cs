using Callisto.Module.Locations;
using Callisto.Module.Vendor.Interfaces;
using Callisto.Module.Vendor.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedKernel.Enum;
using Callisto.SharedKernel.Extensions;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Person;
using Callisto.SharedModels.Session;
using Callisto.SharedModels.Vendor;
using Callisto.SharedModels.Vendor.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Module.Vendor
{
    /// <summary>
    /// Defines the <see cref="VendorModule" />
    /// </summary>
    public class VendorModule : IVendorModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VendorModule"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonProvider{VendorMember}"/></param>
        public VendorModule(ICallistoSession session,
            IPersonProvider<VendorMember> personProvider,
            IVendorRepository vendorRepo
            )
        {
            PersonProvider = personProvider;
            Session = session;
            VendorRepo = vendorRepo;
        }

        /// <summary>
        /// Gets the PersonProvider
        /// </summary>
        public IPersonProvider<VendorMember> PersonProvider { get; }

        /// <summary>
        /// Gets the Session
        /// </summary>
        public ICallistoSession Session { get; }

        /// <summary>
        /// Gets the VendorRepo
        /// </summary>
        public IVendorRepository VendorRepo { get; }

        /// <summary>
        /// The AddVendorMember
        /// </summary>
        /// <param name="model">The <see cref="AddVendorViewModel"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<RequestResult> AddVendorMember(AddVendorViewModel model)
        {
            var person = ModelFactory.CreateVendorMember(model, Session.CurrentCompanyRef);
            using (var tran = await VendorRepo.BeginTransaction())
            {
                if (model.CreateAccount)
                {
                    var createModel = ModelFactory.CreateVendorUser(model, Session.Authentication.GenerateRandomPassword());

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

                await PersonProvider.AddPerson(person);

                tran.Commit();
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The RemoveVendorMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<RequestResult> RemoveVendorMember(Guid Id)
        {
            var VendorMember = await PersonProvider.GetPerson(Id);

            if (VendorMember == null)
            {
                throw new InvalidOperationException($"Vendor member not found");
            }

            using (var tran = await VendorRepo.BeginTransaction())
            {
                await Session.Authentication.RemoveAccount(VendorMember.Email);

                await PersonProvider.RemovePerson(VendorMember);
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The UpdateVendorMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public Task<RequestResult> UpdateVendorMember()
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

            using (var tran = await VendorRepo.BeginTransaction())
            {
                var result = await Session.Authentication.UpdateNewProfileAsync(model);
                if (result.Status == RequestStatus.Success)
                {

                    var member = await PersonProvider.GetPersonByUserId(user.Result);
                    if (member == null)
                    {
                        member = ModelFactory.CreateVendorMember(model, Session.CurrentCompanyRef, Session.EmailAddress);

                        await PersonProvider.AddPerson(member);
                    }
                    else
                    {

                        member.FirstName = model.FirstName;
                        member.LastName = model.LastName;
                        member.ModifiedAt = DateTime.Now;

                        await PersonProvider.UpdatePerson(member);
                    }

                    tran.Commit();
                }
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The GetVendorMembers
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{VendorViewModel}}}"/></returns>
        public async Task<RequestResult<IEnumerable<VendorViewModel>>> GetVendorMembers()
        {
            var list = new List<VendorViewModel>();
            var members = await PersonProvider.GetPeople(Session.CurrentCompanyRef);

            foreach (var item in members)
            {
                list.Add(ModelFactory.CreateVendorMember(item));
            }

            return RequestResult<IEnumerable<VendorViewModel>>.Success(list);
        }
    }
}
