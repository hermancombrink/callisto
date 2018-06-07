using System.ComponentModel.DataAnnotations;

namespace Callisto.SharedModels.Auth.ViewModels
{
    /// <summary>
    /// Defines the <see cref="ConfirmAccountViewModel" />
    /// </summary>
    public class ConfirmAccountViewModel
    {
        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Token
        /// </summary>
        [Required]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the ConfirmPassword
        /// </summary>
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
