using Callisto.SharedModels.Person.ViewModel;

namespace Callisto.SharedModels.Vendor.ViewModels
{
    /// <summary>
    /// Defines the <see cref="AddVendorViewModel" />
    /// </summary>
    public class AddVendorViewModel : PersonBaseViewModel
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
