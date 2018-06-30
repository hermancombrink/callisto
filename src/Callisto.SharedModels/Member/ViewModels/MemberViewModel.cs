using Callisto.SharedModels.Person.ViewModel;
using System;

namespace Callisto.SharedModels.Member.ViewModels
{
    /// <summary>
    /// Defines the <see cref="MemberViewModel" />
    /// </summary>
    public class MemberViewModel : PersonBaseViewModel
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
