using AutoMapper;
using Fitshirt.Api.Dtos.Shields;
using Fitshirt.Api.Errors;
using Fitshirt.Domain.Exceptions;
using Fitshirt.Infrastructure.Models.Designs.Entities;
using Fitshirt.Infrastructure.Repositories.Designs;
using Microsoft.AspNetCore.Mvc;

namespace Fitshirt.Api.Controllers;

[Route("api/v1/shields")]
[ApiController]
public class ShieldController : ControllerBase
{
    private readonly IShieldRepository _shieldRepository;
    private readonly IMapper _mapper;

    public ShieldController(IShieldRepository shieldRepository, IMapper mapper)
    {
        _shieldRepository = shieldRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        var data = await _shieldRepository.GetAllAsync();
        
        if (data.Count == 0)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NoEntitiesFoundException(nameof(Shield)));

            return NotFound(errorResponse);
        }
        
        var result = _mapper.Map<List<ShieldResponse>>(data);
        
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDesignByIdAsync(int id)
    {
        var data = await _shieldRepository.GetByIdAsync(id);
        if (data == null)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NotFoundEntityIdException(nameof(Shield), id));

            return NotFound(errorResponse);
        }
        
        var designResponse = _mapper.Map<Shield,ShieldResponse>(data);
        
        return Ok(designResponse);
    }
}