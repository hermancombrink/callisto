using Callisto.SharedModels.Person.ViewModel;
using System;

namespace Callisto.SharedModels.Staff.ViewModels
{
    /// <summary>
    /// Defines the <see cref="StaffViewModel" />
    /// </summary>
    public class StaffViewModel : PersonBaseViewModel
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
