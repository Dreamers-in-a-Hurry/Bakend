using System.ComponentModel.DataAnnotations;

namespace Fitshirt.Api.Dtos.Users;

public class UserRegisterRequest
{
    [Required(ErrorMessage = "Name is required")]
    [StringLength(32, ErrorMessage = "Name must be between at most 32 characters")]
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "Lastname is required")]
    [StringLength(32, ErrorMessage = "Lastname must be between at most 32 characters")]
    public string Lastname { get; set; } = null!;
    
    [Required(ErrorMessage = "Birthdate is required")]
    public DateOnly Birthdate { get; set; }
    
    [Required(ErrorMessage = "Username is required")]
    [StringLength(16, ErrorMessage = "Username must be between 6 and 16 characters", MinimumLength = 6)]
    public string Username { get; set; } = null!;

    [Required(ErrorMessage = "Password is required")]
    [StringLength(32, ErrorMessage = "Password must be between 8 and 32 characters", MinimumLength = 8)]
    public string Password { get; set; } = null!;
    
    [Required(ErrorMessage = "Password confirmation is required")]
    [Compare("Password")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "Email field is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Phone number is required")]
    [StringLength(9, ErrorMessage = "Phone number must be 9 digits", MinimumLength = 9)]
    [Phone]
    public string Cellphone { get; set; } = null!;
}