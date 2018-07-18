using Callisto.Base.Module;
using Callisto.Module.Authentication.Interfaces;
using Callisto.Module.Authentication.Repository.Models;
using Callisto.SharedKernel;
using Callisto.SharedModels.Auth;
using Callisto.SharedModels.Auth.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Callisto.Module.Authentication.Repository
{
    /// <summary>
    /// Defines the <see cref="AuthenticationRepository" />
    /// </summary>
    public class AuthenticationRepository : BaseRepository<ApplicationDbContext>, IAuthenticationRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationRepository"/> class.
        /// </summary>
        /// <param name="contextFactory">The <see cref="Func{ApplicationDbContext}"/></param>
        public AuthenticationRepository(ApplicationDbContext context,
            DbContextOptions<ApplicationDbContext> options,
            IDbConnection connection) : base(context)
        {
            Options = options;
            Connection = connection;
        }

        /// <summary>
        /// Gets the Options
        /// </summary>
        public DbContextOptions<ApplicationDbContext> Options { get; }

        /// <summary>
        /// Gets the Connection
        /// </summary>
        public IDbConnection Connection { get; }

        /// <summary>
        /// The CreateCompany
        /// </summary>
        /// <param name="company">The <see cref="Company"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task CreateCompany(Company company)
        {
            await Context.Companies.AddAsync(company);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The CreateSubscription
        /// </summary>
        /// <param name="subscription">The <see cref="Subscription"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task CreateSubscription(Subscription subscription)
        {
            await Context.Subscriptions.AddAsync(subscription);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The HasSubscription
        /// </summary>
        /// <param name="userId">The <see cref="string"/></param>
        /// <param name="type">The <see cref="UserType"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task<bool> HasSubscription(string userId, UserType type, long companyRefId)
        {
            return await Context.Subscriptions.AnyAsync(c => c.UserId == userId && c.UserType == type && c.CompanyRefId == companyRefId);
        }

        /// <summary>
        /// The UserExists
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public async Task<bool> UserExists(string email)
        {
            return await Context.Users.AnyAsync(c => c.Email.Trim().ToLower() == email.Trim().ToLower());
        }

        /// <summary>
        /// The GetUserByName
        /// </summary>
        /// <param name="email">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{RequestResult{UserViewModel}}"/></returns>
        public async Task<RequestResult<UserViewModel>> GetUserByName(string email)
        {
            var qry = from user in Context.Users
                      join subscription in Context.Subscriptions on user.Id equals subscription.UserId
                      join company in Context.Companies on subscription.CompanyRefId equals company.RefId
                      where user.UserName.ToLower().Trim() == email.ToLower().Trim()
                      && !subscription.Deactivated
                      select new
                      {
                          UserId = user.Id,
                          user.FirstName,
                          user.LastName,
                          user.Email,
                          user.UserName,
                          CompanyName = company.Name,
                          SubscriptionId = subscription.Id,
                          SubscriptionRefId = subscription.RefId,
                          UserType = subscription.UserType,
                          LastAccess = user.LastCompanyLogin,
                          CompanyRefId = company.RefId,
                          Role = subscription.JobRole,
                          EmailVerified = user.EmailConfirmed
                      };

            var lastSubsrition = await qry.FirstOrDefaultAsync(c => c.LastAccess == c.CompanyRefId);

            if (lastSubsrition == null)
            {
                lastSubsrition = await qry.OrderByDescending(c => c.SubscriptionRefId).FirstOrDefaultAsync();
            }

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
                    UserType = lastSubsrition.UserType,
                    CompanyRefId = lastSubsrition.CompanyRefId,
                    SubscriptionRefId = lastSubsrition.SubscriptionRefId,
                    Id = lastSubsrition.UserId,
                    UserName = lastSubsrition.UserName,
                    EmailVerified = lastSubsrition.EmailVerified,
                    JobRole = lastSubsrition.Role
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
        /// The GetUserByID
        /// </summary>
        /// <param name="userId">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{ApplicationUser}"/></returns>
        public async Task<ApplicationUser> GetUserById(string userId)
        {
            return await Context.Users.FirstOrDefaultAsync(c => c.Id == userId);
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
        /// The UpdateSubscription
        /// </summary>
        /// <param name="subscriptions">The <see cref="Subscription"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task UpdateSubscription(Subscription subscriptions)
        {
            Context.Subscriptions.Attach(subscriptions);
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
        /// The GetSubscription
        /// </summary>
        /// <param name="userId">The <see cref="string"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{Subscription}"/></returns>
        public async Task<Subscription> GetSubscription(string userId, long companyRefId)
        {
            return await Context.Subscriptions.FirstOrDefaultAsync(c => c.UserId == userId && c.CompanyRefId == companyRefId);
        }

        /// <summary>
        /// The RemoveAccount
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveAccount(ApplicationUser user)
        {
            user.UserName = Crypto.GetStringSha256Hash(user.UserName, user.Id);
            user.Email = Crypto.GetStringSha256Hash(user.Email, user.Id);
            user.NormalizedEmail = Crypto.GetStringSha256Hash(user.Email, user.Id);
            user.NormalizedUserName = Crypto.GetStringSha256Hash(user.Email, user.Id);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The RemoveSubscription
        /// </summary>
        /// <param name="user">The <see cref="ApplicationUser"/></param>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemoveSubscription(ApplicationUser user, long companyRefId)
        {
            var subscription = await GetSubscription(user.Id, companyRefId);
            if (subscription != null)
            {
                Context.Subscriptions.Remove(subscription);
            }
            await Context.SaveChangesAsync();
        }
    }
}
