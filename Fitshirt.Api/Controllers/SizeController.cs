using AutoMapper;
using Fitshirt.Api.Dtos.Sizes;
using Fitshirt.Api.Errors;
using Fitshirt.Domain.Exceptions;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Repositories.Common.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Fitshirt.Api.Controllers;

[Route("api/v1/sizes")]
[ApiController]
public class SizeController : ControllerBase
{
    private readonly ISizeRepository _sizeRepository;
    private readonly IMapper _mapper;

    public SizeController(ISizeRepository sizeRepository, IMapper mapper)
    {
        _sizeRepository = sizeRepository;
        _mapper = mapper;
    }
    
    /// GET: api/v1/sizes
    /// <summary>
    /// Get a List of All Shirt Sizes.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetSizesAsync()
    {
        var data = await _sizeRepository.GetAllAsync();
        
        if (data.Count == 0)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NoEntitiesFoundException(nameof(Size)));

            return NotFound(errorResponse);
        }
        
        var result = _mapper.Map<List<SizeResponse>>(data);
        
        return Ok(result);
    }
    
    /// GET: api/v1/sizes/{id}
    /// <summary>
    /// Get a List of Shirt Sizes by Id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSizeByIdAsync(int id)
    {
        var data = await _sizeRepository.GetByIdAsync(id);
        if (data == null)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NotFoundEntityIdException(nameof(Size), id));

            return NotFound(errorResponse);
        }
        
        var designResponse = _mapper.Map<Size, SizeResponse>(data);
        
        return Ok(designResponse);
    }
}