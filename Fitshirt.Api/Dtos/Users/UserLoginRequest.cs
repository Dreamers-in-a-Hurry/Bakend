using System.ComponentModel.DataAnnotations;

namespace Fitshirt.Api.Dtos.Users;

public class UserLoginRequest
{
    [Required(ErrorMessage = "This field is required")]
    [StringLength(16, ErrorMessage = "Username must be between 6 and 16 characters", MinimumLength = 6)]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "This field is required")]
    [StringLength(32, ErrorMessage = "Password must be between 8 and 32 characters", MinimumLength = 8)]
    public string Password { get; set; } = null!;
}