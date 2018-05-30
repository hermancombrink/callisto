using Callisto.Module.Staff.Repository.Models;
using Callisto.SharedModels.Person;

namespace Callisto.Module.Staff.Repository
{
    /// <summary>
    /// Defines the <see cref="StaffRepository" />
    /// </summary>
    public class StaffRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StaffRepository"/> class.
        /// </summary>
        /// <param name="personProvider">The <see cref="IPersonRepository{StaffMember}"/></param>
        public StaffRepository(IPersonRepository<StaffMember> personRepo)
        {
            PersonRepo = personRepo;
        }

        /// <summary>
        /// Gets the PersonProvider
        /// </summary>
        public IPersonRepository<StaffMember> PersonRepo { get; }
    }
}
