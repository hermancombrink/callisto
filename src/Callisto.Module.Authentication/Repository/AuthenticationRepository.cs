using Callisto.Module.Authentication.Interfaces;
using Callisto.Module.Authentication.ViewModels;
using Callisto.SharedKernel;
using System;
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
        /// The CanRegisterNewCompany
        /// </summary>
        /// <returns>The <see cref="Task{bool}"/></returns>
        public async Task<RequestResult<long>> RegisterNewAccountAsync(RegisterViewModel model)
        {
            if (Context.Users.Any(c => c.UserName == model.Email || c.Email == c.Email))
            {
                return RequestResult<long>.Failed("User already exists");
            }

            if (Context.Companies.Any(c => c.Name == model.CompanyName))
            {
                return RequestResult<long>.Failed("Company already exists");
            }

            var company = ModelFactory.CreateCompany(model);
            await Context.Companies.AddAsync(company);
            var subscription = ModelFactory.GetSubscription(company);
            await Context.Subscriptions.AddAsync(subscription);

            await Context.SaveChangesAsync();

            return RequestResult<long>.Success(company.RefId);
        }
    }
}
