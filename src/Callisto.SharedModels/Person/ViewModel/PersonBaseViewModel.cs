namespace Callisto.SharedModels.Person.ViewModel
{
    /// <summary>
    /// Defines the <see cref="PersonBaseViewModel" />
    /// </summary>
    public abstract class PersonBaseViewModel
    {
        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName
        /// </summary>
        public string LastName { get; set; }
    }
}
