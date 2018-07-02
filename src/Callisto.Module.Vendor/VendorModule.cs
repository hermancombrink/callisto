using Callisto.Module.Locations;
using Callisto.Module.Vendor.Interfaces;
using Callisto.Module.Vendor.Repository.Models;
using Callisto.Provider.Person;
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
    public class VendorModule : PersonModule<VendorMember>, IVendorModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VendorModule"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonModule{VendorMember}"/></param>
        public VendorModule(ICallistoSession session,
            IVendorRepository vendorRepo
            ) : base(vendorRepo)
        {
            Session = session;
            VendorRepo = vendorRepo;
        }

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
            using (var tran = VendorRepo.BeginTransaction())
            {
                if (model.CreateAccount)
                {
                    var createModel = ModelFactory.CreateVendorUser(model, Session.Authentication.GenerateRandomPassword());

                    var user = await Session.Authentication.GetUserId(model.Email);
                    RequestResult result = RequestResult.Success();
                    if (!user.IsSuccess())
                    {
                        result = await Session.Authentication.RegisterUserWithCurrentCompanyAsync(createModel);

                        if (!result.IsSuccess())
                        {
                            return result;
                        }

                        user = await Session.Authentication.GetUserId(model.Email);
                        if (!user.IsSuccess())
                        {
                            throw new InvalidOperationException($"Failed to find user");
                        }
                    }
                    else
                    {
                        result = await Session.Authentication.CreateSubscription(user.Result, SharedModels.Auth.UserType.Vendor);

                        if (!result.IsSuccess())
                        {
                            return result;
                        }
                    }

                    person.UserId = user.Result;

                    if (model.SendLink)
                    {
                        var message = Session.Notification.CreateSimpleMessage(NotificationType.AccountInvite, person.Email, result.Result.ToTokenDictionary("~token~"));

                        Session.MessageCoordinator.Publish(message, Session);
                    }
                }

                await AddPerson(person);

                VendorRepo.CommitTransaction();
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The RemoveVendorMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<RequestResult> RemoveVendorMember(Guid Id)
        {
            var VendorMember = await GetPerson(Id);

            if (VendorMember == null)
            {
                throw new InvalidOperationException($"Vendor member not found");
            }

            using (var tran = VendorRepo.BeginTransaction())
            {
                await Session.Authentication.RemoveSubscription(VendorMember.Email);

                await RemovePerson(VendorMember);
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

            using (var tran = VendorRepo.BeginTransaction())
            {
                var result = await Session.Authentication.UpdateNewProfileAsync(model);
                if (result.Status == RequestStatus.Success)
                {

                    var member = await GetPersonByUserId(user.Result);
                    if (member == null)
                    {
                        member = ModelFactory.CreateVendorMember(model, Session.CurrentCompanyRef, Session.EmailAddress);

                        await AddPerson(member);
                    }
                    else
                    {
                        member.FirstName = model.FirstName;
                        member.LastName = model.LastName;
                        member.ModifiedAt = DateTime.Now;

                        await UpdatePerson(member);
                    }

                    VendorRepo.CommitTransaction();
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
            var members = await GetPeople(Session.CurrentCompanyRef);

            foreach (var item in members)
            {
                list.Add(ModelFactory.CreateVendorMember(item));
            }

            return RequestResult<IEnumerable<VendorViewModel>>.Success(list);
        }
    }
}
