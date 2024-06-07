using System.ComponentModel.DataAnnotations;

namespace Fitshirt.Api.Dtos.Posts;

public class PostRequest
{
    [Required(ErrorMessage = "This field is required")] 
    [MaxLength(32)] 
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "This field is required")]
    [Url(ErrorMessage = "The field must be a current url")] 
    public string Image { get; set; } = null!;
    
    [Required(ErrorMessage = "This field is required")]
    [Range(1, 1000, ErrorMessage = "The number field must be a positive integer")]
    public int Stock { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    [Range(0.01, 9999.99, ErrorMessage = "The number field must be a positive number")]
    public double Price { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int CategoryId { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int ColorId { get; set; }
    
    [Required(ErrorMessage = "This field is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int UserId { get; set; }
    
    [Required]
    public List<int> SizeIds { get; set; }
}