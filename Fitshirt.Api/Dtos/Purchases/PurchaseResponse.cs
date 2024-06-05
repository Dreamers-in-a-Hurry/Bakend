using Fitshirt.Api.Dtos.Items;
using Fitshirt.Api.Dtos.Users;

namespace Fitshirt.Api.Dtos.Purchases;

public class PurchaseResponse
{
    public int Id { get; set; }
    public DateTime PurchaseDate { get; set; }
    public UserResponse User { get; set; }
    public List<ItemResponse> Items { get; set; }
}