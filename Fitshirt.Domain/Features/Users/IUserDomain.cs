using Fitshirt.Domain.Features.Common;
using Fitshirt.Infrastructure.Models.Users;

namespace Fitshirt.Domain.Features.Users;

public interface IUserDomain : IBaseDomain<User>
{
    Task<User> VerifyLoginRequestAsync(User user);
}