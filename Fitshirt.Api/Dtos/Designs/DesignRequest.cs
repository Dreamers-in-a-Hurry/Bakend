using System.ComponentModel.DataAnnotations;

namespace Fitshirt.Api.Dtos.Designs;

public class DesignRequest
{
    [Required(ErrorMessage = "This field is required")] 
    [MaxLength(32)] 
    public string Name { get; set; } = null!;
    
    [Required(ErrorMessage = "This field is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int PrimaryColorId { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int SecondaryColorId { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int TertiaryColorId { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int UserId { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int ShieldId { get; set; }
}