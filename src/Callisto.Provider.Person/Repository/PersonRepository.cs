using Callisto.SharedModels.Models;
using Callisto.SharedModels.Person;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Callisto.Provider.Person.Repository
{
    /// <summary>
    /// Defines the <see cref="PersonRepository" />
    /// </summary>
    public class PersonRepository<T> : IPersonRepository<T> where T : BasePerson
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonRepository{T}"/> class.
        /// </summary>
        /// <param name="context">The <see cref="PersonDbContext"/></param>
        public PersonRepository(PersonDbContext<T> context)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the Context
        /// </summary>
        private PersonDbContext<T> Context { get; }

        /// <summary>
        /// The AddPerson
        /// </summary>
        /// <param name="person">The <see cref="T"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task AddPerson(T person)
        {
            await Context.People.AddAsync(person);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The GetPeople
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{T}}"/></returns>
        public async Task<IEnumerable<T>> GetPeople(long companyRefId)
        {
            var result = await Context.People.Where(c => c.CompanyRefId == companyRefId).ToListAsync();

            return result as IEnumerable<T>;
        }

        /// <summary>
        /// The GetPerson
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{T}"/></returns>
        public async Task<T> GetPerson(long refId)
        {
            return await Context.People.FirstOrDefaultAsync(c => c.RefId == refId);
        }

        /// <summary>
        /// The GetPersonByUserId
        /// </summary>
        /// <param name="userId">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{T}"/></returns>
        public async Task<T> GetPersonByUserId(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return await Context.People.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        /// <summary>
        /// The GetPerson
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{T}"/></returns>
        public async Task<T> GetPerson(Guid id)
        {
            return await Context.People.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// The RemovePerson
        /// </summary>
        /// <param name="person">The <see cref="T"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemovePerson(T person)
        {
            Context.People.Remove(person);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// The UpdatePerson
        /// </summary>
        /// <param name="person">The <see cref="T"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task UpdatePerson(T person)
        {
            Context.People.Attach(person);
            await Context.SaveChangesAsync();
        }
    }
}
