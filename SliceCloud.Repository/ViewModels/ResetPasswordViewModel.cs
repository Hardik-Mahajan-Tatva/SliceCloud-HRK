using System.ComponentModel.DataAnnotations;

namespace SliceCloud.Repository.ViewModels;

public class ResetPasswordViewModel
{
    [Required(ErrorMessage = "Reset token is required.")]
    public string? Token { get; set; }

    [Required(ErrorMessage = "New password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
    public string? NewPassword { get; set; }

    [Required(ErrorMessage = "Confirm password is required.")]
    [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
    public string? ConfirmPassword { get; set; }
}
