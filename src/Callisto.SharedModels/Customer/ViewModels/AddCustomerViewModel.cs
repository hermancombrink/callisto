using Callisto.SharedModels.Person.ViewModel;

namespace Callisto.SharedModels.Customer.ViewModels
{
    /// <summary>
    /// Defines the <see cref="AddCustomerViewModel" />
    /// </summary>
    public class AddCustomerViewModel : PersonBaseViewModel
    {
        /// <summary>
        /// Gets or sets a value indicating whether CreateAccount
        /// </summary>
        public bool CreateAccount { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether SendLink
        /// </summary>
        public bool SendLink { get; set; }
    }
}
