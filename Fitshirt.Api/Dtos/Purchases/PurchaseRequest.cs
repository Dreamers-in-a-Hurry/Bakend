using Fitshirt.Api.Dtos.Items;
using System.ComponentModel.DataAnnotations;

namespace Fitshirt.Api.Dtos.Purchases;

public class PurchaseRequest
{
    [Required(ErrorMessage = "The user Id is required")]
    [Range(1, int.MaxValue, ErrorMessage = "The number field must be a positive integer")]
    public int UserId { get; set; }
    
    [Required(ErrorMessage = "Items list is required")]
    public List<ItemRequest> Items { get; set; }
}