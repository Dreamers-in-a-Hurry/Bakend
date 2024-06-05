using Fitshirt.Domain.Features.Users;
using Fitshirt.Infrastructure.Repositories.Users;
using Moq;

namespace Fitshirt.Domain.Tests.Features.Users;

public class UserDomainTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IServiceRepository> _serviceRepositoryMock;
    private readonly Mock<IRoleRepository> _roleRepositoryMock;
    private readonly UserDomain _userDomain;

    public UserDomainTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _serviceRepositoryMock = new Mock<IServiceRepository>();
        _roleRepositoryMock = new Mock<IRoleRepository>();

        _userDomain = new UserDomain(
            _userRepositoryMock.Object,
            _serviceRepositoryMock.Object,
            _roleRepositoryMock.Object
        );
    }

}