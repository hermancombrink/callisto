using Callisto.Module.Authentication.Interfaces;
using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedModels.Auth.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Threading.Tasks;

namespace Callisto.Module.Authentication.Repository
{
    /// <summary>
    /// Defines the <see cref="AuthenticationRepository" />
    /// </summary>
    public class AuthenticationRepository : IAuthenticationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRepository"/> class.
        /// </summary>
        /// <param name="contextFactory">The <see cref="Func{ApplicationDbContext}"/></param>
        public AuthenticationRepository(ApplicationDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the Context
        /// </summary>
        private ApplicationDbContext Context { get; }

        /// <summary>
        /// The BeginTransaction
        /// </summary>
        /// <returns>The <see cref="Task{IDbContextTransaction}"/></returns>
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await Context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// The CanRegisterNewCompany
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public async Task<RequestResult<long>> RegisterNewAccountAsync(RegisterViewModel model)
        {
            if (Context.Users.Any(c => c.UserName == model.Email || c.Email == model.Email))
            {
                return RequestResult<long>.Failed("User already exists");
            }

            var company = ModelFactory.CreateCompany(model);
            await Context.Companies.AddAsync(company);
            await Context.SaveChangesAsync();
            var subscription = ModelFactory.CreateSubscription(company);
            await Context.Subscriptions.AddAsync(subscription);
            await Context.SaveChangesAsync();

            return RequestResult<long>.Success(company.RefId);
        }

        /// <summary>
        /// The GetUserByName
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult{UserViewModel}}"/></returns>
        public async Task<RequestResult<UserViewModel>> GetUserByName(string email)
        {
            var qry = from user in Context.Users
                      join company in Context.Companies on user.CompanyRefId equals company.RefId
                      join subscription in Context.Subscriptions on company.RefId equals subscription.CompanyRefId
                      where user.UserName.ToLower().Trim() == email.ToLower().Trim()
                      && !user.Deactivated
                      select new
                      {
                          user.FirstName,
                          user.LastName,
                          user.Email,
                          CompanyName = company.Name,
                          SubscriptionId = subscription.Id,
                          SubscriptionRefId = subscription.RefId,
                          UserType = user.UserType
                      };

            var lastSubsrition = await qry.OrderByDescending(c => c.SubscriptionRefId).FirstOrDefaultAsync();

            if (lastSubsrition == null)
            {
                return RequestResult<UserViewModel>.Failed($"Failed to find user");
            }
            else
            {
                return RequestResult<UserViewModel>.Success(new UserViewModel()
                {
                    Company = lastSubsrition.CompanyName,
                    Email = lastSubsrition.Email,
                    FirstName = lastSubsrition.FirstName,
                    LastName = lastSubsrition.LastName,
                    SubscriptionId = lastSubsrition.SubscriptionId,
                    UserType = lastSubsrition.UserType
                });
            }
        }

        /// <summary>
        /// The UpdateUser
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task UpdateUser(ApplicationUser user)
        {
            Context.Users.Attach(user);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The GetUser
        /// </summary>
        /// <param name="userName">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUser> GetUser(string userName)
        {
            return await Context.Users.FirstOrDefaultAsync(c => c.UserName == userName);
        }

        /// <summary>
        /// The UpdateCompany
        /// </summary>
        /// <param name="company">The <see cref="Company"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task UpdateCompany(Company company)
        {
            Context.Companies.Attach(company);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The GetCompany
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Company}"/></returns>
        public async Task<Company> GetCompany(long refId)
        {
            return await Context.Companies.FindAsync(refId);
        }

        /// <summary>
        /// The RemoveAccount
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveAccount(ApplicationUser user)
        {
            user.Deactivated = true;
            user.UserName = Crypto.GetStringSha256Hash(user.UserName, user.Id);
            user.Email = Crypto.GetStringSha256Hash(user.Email, user.Id);
            user.NormalizedEmail = Crypto.GetStringSha256Hash(user.Email, user.Id);
            user.NormalizedUserName = Crypto.GetStringSha256Hash(user.Email, user.Id);
            await Context.SaveChangesAsync();
        }
    }
}
