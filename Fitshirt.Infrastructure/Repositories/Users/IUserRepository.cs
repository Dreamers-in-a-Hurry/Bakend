using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Repositories.Common;

namespace Fitshirt.Infrastructure.Repositories.Users;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByPhoneNumberAsync(string phoneNumber);
    Task<User?> GetUserByUsernameAsync(string username);
}