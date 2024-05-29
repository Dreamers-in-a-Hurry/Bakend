namespace Fitshirt.Api.Dtos.Users;

public class UserResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string Cellphone { get; set; }
    public DateOnly BirthDate { get; set; }
}