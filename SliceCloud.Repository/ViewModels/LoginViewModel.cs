using System.ComponentModel.DataAnnotations;

namespace SliceCloud.Repository.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is required")]
    [StringLength(100, ErrorMessage = "Email cannot be longer than 100 characters.")]
    [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email Address format")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [DataType(DataType.Password)]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters long.")]
    public string Password { get; set; } = string.Empty;
    public bool RememberMe { get; set; }
}