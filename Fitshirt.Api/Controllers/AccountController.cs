using AutoMapper;
using Fitshirt.Api.Dtos.Users;
using Fitshirt.Domain.Features.Users;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Mvc;

namespace Fitshirt.Api.Controllers;

[ApiController]
[Route("api/v1/account")]
public class AccountController : ControllerBase
{
    private readonly IUserDomain _userDomain;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public AccountController(IUserDomain userDomain, IUserRepository userRepository, IMapper mapper)
    {
        _userDomain = userDomain;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = _mapper.Map<UserRegisterRequest, User>(request);
        var result = await _userDomain.AddAsync(user);
        
        return Ok(result);
    }
}