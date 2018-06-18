using Callisto.SharedModels.Person.ViewModel;
using System;

namespace Callisto.SharedModels.Customer.ViewModels
{
    /// <summary>
    /// Defines the <see cref="CustomerViewModel" />
    /// </summary>
    public class CustomerViewModel : PersonBaseViewModel
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the ParentId
        /// </summary>
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the PictureUrl
        /// </summary>
        public string PictureUrl { get; set; }
    }
}
