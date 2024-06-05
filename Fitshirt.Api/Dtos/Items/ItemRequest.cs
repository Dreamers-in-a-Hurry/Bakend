namespace Fitshirt.Api.Dtos.Items;
using System.ComponentModel.DataAnnotations;

public class ItemRequest
{
    [Required(ErrorMessage = "The post Id is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int PostId { get; set; }
    
    [Required(ErrorMessage = "The size Id is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int SizeId { get; set; }
    
    [Required(ErrorMessage = "The quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int Quantity { get; set; }
}