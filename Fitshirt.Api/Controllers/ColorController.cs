using AutoMapper;
using Fitshirt.Api.Dtos.Colors;
using Fitshirt.Api.Errors;
using Fitshirt.Domain.Exceptions;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Repositories.Common.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Fitshirt.Api.Controllers;

[Route("api/v1/colors")]
[ApiController]
public class ColorController : ControllerBase
{
    private readonly IColorRepository _colorRepository;
    private readonly IMapper _mapper;

    public ColorController(IColorRepository colorRepository, IMapper mapper)
    {
        _colorRepository = colorRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        var data = await _colorRepository.GetAllAsync();
        
        if (data.Count == 0)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NoEntitiesFoundException(nameof(Color)));

            return NotFound(errorResponse);
        }
        
        var result = _mapper.Map<List<ColorResponse>>(data);
        
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDesignByIdAsync(int id)
    {
        var data = await _colorRepository.GetByIdAsync(id);
        if (data == null)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NotFoundEntityIdException(nameof(Color), id));

            return NotFound(errorResponse);
        }
        
        var designResponse = _mapper.Map<Color,ColorResponse>(data);
        
        return Ok(designResponse);
    }
}