using AutoMapper;
using Fitshirt.Api.Dtos.Users;
using Fitshirt.Domain.Exceptions;
using Fitshirt.Domain.Features.Users;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Mvc;

namespace Fitshirt.Api.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IUserDomain _userDomain;
    private readonly IMapper _mapper;

    public UserController(IUserRepository userRepository, IUserDomain userDomain, IMapper mapper)
    {
        _userRepository = userRepository;
        _userDomain = userDomain;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync()
    {
        var data = await _userRepository.GetAllAsync();
        var result = _mapper.Map<List<UserResponse>>(data);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> PutUserAsync(int id, [FromBody] UserRequest request)
    {
        var userToUpdate = await _userRepository.GetByIdAsync(id);
        if (userToUpdate == null)
        {
            throw new NotFoundEntityIdException(nameof(User), id);
        }

        _mapper.Map(request, userToUpdate, typeof(UserRequest), typeof(User));

        var result = await _userDomain.UpdateAsync(id, userToUpdate);
        return Ok(result);
    }
}