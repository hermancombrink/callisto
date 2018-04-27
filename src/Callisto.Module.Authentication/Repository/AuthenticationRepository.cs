using Callisto.Module.Authentication.Interfaces;
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

            if (Context.Companies.Any(c => c.Name == model.CompanyName))
            {
                return RequestResult<long>.Failed("Company already exists");
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
                      select new
                      {
                          user.FirstName,
                          user.LastName,
                          user.Email,
                          CompanyName = company.Name,
                          SubscriptionId = subscription.Id,
                          SubscriptionRefId = subscription.RefId
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
                    SubscriptionId = lastSubsrition.SubscriptionId
                });
            }
        }
    }
}
