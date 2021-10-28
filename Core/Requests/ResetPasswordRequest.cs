using Core.Common;
using System.ComponentModel.DataAnnotations;

namespace Core.Requests
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@SettingsVariables.PasswordExpression,
            ErrorMessage = "Password is not valid (at least one digit, upper letter and lenght >= 8)")]
        public string newPassword { get; set; }
        [Compare(nameof(newPassword), ErrorMessage = "Password doesn't match.")]
        [Required(ErrorMessage = "Confirm password is required")]
        public string confirmNewPassword { get; set; }
    }
}
