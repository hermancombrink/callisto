using Callisto.Module.Customer.Interfaces;
using Callisto.Module.Customer.Repository.Models;
using Callisto.Provider.Person;
using Callisto.SharedKernel;
using Callisto.SharedKernel.Enum;
using Callisto.SharedKernel.Extensions;
using Callisto.SharedModels.Auth.ViewModels;
using Callisto.SharedModels.Customer;
using Callisto.SharedModels.Customer.ViewModels;
using Callisto.SharedModels.Notification.Enum;
using Callisto.SharedModels.Session;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Transactions;

namespace Callisto.Module.Customer
{
    /// <summary>
    /// Defines the <see cref="CustomerModule" />
    /// </summary>
    public class CustomerModule : PersonModule<CustomerMember>, ICustomerModule
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerModule"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonModule{CustomerMember}"/></param>
        public CustomerModule(ICallistoSession session,
            ICustomerRepository customerRepo
            ) : base(customerRepo)
        {
            Session = session;
            CustomerRepo = customerRepo;
        }

        /// <summary>
        /// Gets the Session
        /// </summary>
        public ICallistoSession Session { get; }

        /// <summary>
        /// Gets the CustomerRepo
        /// </summary>
        public ICustomerRepository CustomerRepo { get; }

        /// <summary>
        /// The AddCustomerMember
        /// </summary>
        /// <param name="model">The <see cref="AddCustomerViewModel"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<RequestResult> AddCustomerMember(AddCustomerViewModel model)
        {
            var person = ModelFactory.CreateCustomerMember(model, Session.CurrentCompanyRef);
            using (var tran = Session.GetSessionTransaction())
            {
                if (model.CreateAccount)
                {
                    var createModel = ModelFactory.CreateCustomerUser(model, Session.Authentication.GenerateRandomPassword());

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

                tran.Complete();
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The RemoveCustomerMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<RequestResult> RemoveCustomerMember(Guid Id)
        {
            var CustomerMember = await GetPerson(Id);

            if (CustomerMember == null)
            {
                throw new InvalidOperationException($"Customer member not found");
            }

            await RemovePerson(CustomerMember);

            return RequestResult.Success();
        }

        /// <summary>
        /// The UpdateCustomerMember
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        public Task<RequestResult> UpdateCustomerMember()
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

            using (var tran = Session.GetSessionTransaction())
            {
                var result = await Session.Authentication.UpdateNewProfileAsync(model);
                if (result.Status == RequestStatus.Success)
                {

                    var member = await GetPersonByUserId(user.Result);
                    if (member == null)
                    {
                        member = ModelFactory.CreateCustomerMember(model, Session.CurrentCompanyRef, Session.EmailAddress);

                        await AddPerson(member);
                    }
                    else
                    {

                        member.FirstName = model.FirstName;
                        member.LastName = model.LastName;
                        member.ModifiedAt = DateTime.Now;

                        await UpdatePerson(member);
                    }

                    tran.Complete();
                }
            }

            return RequestResult.Success();
        }

        /// <summary>
        /// The GetCustomerMembers
        /// </summary>
        /// <returns>The <see cref="Task{RequestResult{IEnumerable{CustomerViewModel}}}"/></returns>
        public async Task<RequestResult<IEnumerable<CustomerViewModel>>> GetCustomerMembers()
        {
            var list = new List<CustomerViewModel>();
            var members = await GetPeople(Session.CurrentCompanyRef);

            foreach (var item in members)
            {
                list.Add(ModelFactory.CreateCustomerMember(item));
            }

            return RequestResult<IEnumerable<CustomerViewModel>>.Success(list);
        }
    }
}
