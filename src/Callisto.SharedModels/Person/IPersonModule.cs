using Callisto.SharedModels.Base;
using Callisto.SharedModels.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.SharedModels.Person
{
    /// <summary>
    /// Defines the <see cref="IPersonProvider" />
    /// </summary>
    public interface IPersonModule<T> : IBaseModule where T : BasePerson
    {
        /// <summary>
        /// The AddPerson
        /// </summary>
        /// <param name="person">The <see cref="T"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task AddPerson(T person);

        /// <summary>
        /// The RemovePerson
        /// </summary>
        /// <param name="person">The <see cref="T"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task RemovePerson(T person);

        /// <summary>
        /// The UpdatePerson
        /// </summary>
        /// <param name="person">The <see cref="T"/></param>
        /// <returns>The <see cref="Task"/></returns>
        Task UpdatePerson(T person);

        /// <summary>
        /// The GetPerson
        /// </summary>
        /// <param name="refId">The <see cref="long"/></param>
        /// <returns>The <see cref="Task{T}"/></returns>
        Task<T> GetPerson(long refId);

        /// <summary>
        /// The GetPerson
        /// </summary>
        /// <param name="id">The <see cref="Guid"/></param>
        /// <returns>The <see cref="Task{T}"/></returns>
        Task<T> GetPerson(Guid id);

        /// <summary>
        /// The GetPersonByUserId
        /// </summary>
        /// <param name="userId">The <see cref="string"/></param>
        /// <returns>The <see cref="Task{T}"/></returns>
        Task<T> GetPersonByUserId(string userId);

        /// <summary>
        /// The GetPeople
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{T}}"/></returns>
        Task<IEnumerable<T>> GetPeople(long companyRefId);
    }
}
