using Callisto.Base.Module;
using Callisto.SharedModels.Models;
using Callisto.SharedModels.Person;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Provider.Person
{
    /// <summary>
    /// Defines the <see cref="PersonModule{T}" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class PersonModule<T> : BaseModule, IPersonModule<T> where T : BasePerson
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PersonModule{T}"/> class.
        /// </summary>
        /// <param name="personRepo">The <see cref="IPersonRepository{T}"/></param>
        public PersonModule(IPersonRepository<T> personRepo) : base(personRepo)
        {
            PersonRepo = personRepo;
        }

        /// <summary>
        /// Gets the PersonRepo
        /// </summary>
        public IPersonRepository<T> PersonRepo { get; }

        /// <summary>
        /// The AddPerson
        /// </summary>
        /// <param name="person">The <see cref="T"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task AddPerson(T person)
        {
            await PersonRepo.AddPerson(person);
        }

        /// <summary>
        /// The GetPeople
        /// </summary>
        /// <param name="companyRefId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{IEnumerable{T}}"/></returns>
        public async Task<IEnumerable<T>> GetPeople(long companyRefId)
        {
            return await PersonRepo.GetPeople(companyRefId);
        }

        /// <summary>
        /// The GetPerson
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{T}"/></returns>
        public async Task<T> GetPerson(long refId)
        {
            return await PersonRepo.GetPerson(refId);
        }

        /// <summary>
        /// The GetPersonByUserId
        /// </summary>
        /// <param name="userId">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{T}"/></returns>
        public async Task<T> GetPersonByUserId(string userId)
        {
            return await PersonRepo.GetPersonByUserId(userId);
        }

        /// <summary>
        /// The GetPerson
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{T}"/></returns>
        public async Task<T> GetPerson(Guid id)
        {
            return await PersonRepo.GetPerson(id);
        }

        /// <summary>
        /// The RemovePerson
        /// </summary>
        /// <param name="person">The <see cref="T"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task RemovePerson(T person)
        {
            await PersonRepo.RemovePerson(person);
        }

        /// <summary>
        /// The UpdatePerson
        /// </summary>
        /// <param name="person">The <see cref="T"/></param>
        /// <returns>The <see cref="Task"/></returns>
        public async Task UpdatePerson(T person)
        {
            await PersonRepo.UpdatePerson(person);
        }
    }
}
