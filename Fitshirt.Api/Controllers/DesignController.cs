using System.ComponentModel;
using AutoMapper;
using Fitshirt.Api.Dtos.Designs;
using Fitshirt.Api.Dtos.Posts;
using Fitshirt.Api.Errors;
using Fitshirt.Domain.Exceptions;
using Fitshirt.Domain.Features.Designs;
using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Repositories.Designs;
using Microsoft.AspNetCore.Mvc;

namespace Fitshirt.Api.Controllers;

[Route("api/v1/designs")]
[ApiController]
public class DesignController: ControllerBase
{
    private readonly IDesignDomain _designDomain;
    private readonly IDesignRepository _designRepository;
    private readonly IMapper _mapper;

    public DesignController(IDesignDomain designDomain, IDesignRepository designRepository, IMapper mapper)
    {
        _designDomain = designDomain;
        _designRepository = designRepository;
        _mapper = mapper;
    }
    /// GET: api/v1/designs
    /// <summary>
    /// Get a List of Designs Created.
    /// </summary>
    [HttpGet]
    [Description("Get designs in view mode for catalogue")]
    public async Task<ActionResult> GetDesignsAsync()
    {
        var data = await _designRepository.GetAllAsync();
        
        if (data.Count == 0)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NoEntitiesFoundException(nameof(Design)));

            return NotFound(errorResponse);
        }
        
        var result = _mapper.Map<List<ShirtVm>>(data);
        
        return Ok(result);
    }
    /// /// GET: api/v1/designs/{id}
    /// <summary>
    /// Get a list of Designs by Id.
    /// </summary>
    [HttpGet("{id}")]
    [Description("Get designs in details for catalogue description and for designs details")]
    public async Task<IActionResult> GetDesignByIdAsync(int id)
    {
        var data = await _designRepository.GetByIdAsync(id);
        if (data == null)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NotFoundEntityIdException(nameof(Design), id));

            return NotFound(errorResponse);
        }
        
        var designResponse = _mapper.Map<Design,DesignResponse>(data);
        
        return Ok(designResponse);
    }
    /// /// GET: api/v1/designs/SearchByUser
    /// <summary>
    /// Get a Design by UserId.
    /// </summary>
    [HttpGet]
    [Route("SearchByUser")]
    [Description("Get designs in view mode published by an user")]
    public async Task<IActionResult> GetDesignByUserIdAsync(int userId)
    {
        var data = await _designRepository.GetDesignsByUserId(userId);
        
        if (data.Count == 0)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NotFoundEntityIdException(nameof(User), userId));

            return NotFound(errorResponse);
        }
        
        var result = _mapper.Map<List<ShirtVm>>(data);
        return Ok(result);
    }
    /// POST: api/v1/designs
    /// <summary>
    /// Create a Design
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/designs
    ///     {
    ///        "name": "RopaPersonalizada",
    ///        "primaryColorId": 1,
    ///        "secondaryColorId": 2,
    ///        "tertiaryColorId": 3,
    ///        "userId": 1,
    ///        "shieldId": 6
    ///     }
    ///
    ///     True
    ///
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> PostDesignAsync([FromBody] DesignRequest request)
    {
        var design = _mapper.Map<DesignRequest, Design>(request);
        var result = await _designDomain.AddDesignAsync(design);
        return StatusCode(StatusCodes.Status201Created, result);
    }
    /// /// PUT: api/v1/designs
    /// <summary>
    /// Modify a Design
    /// </summary>
    [HttpPut]
    public async Task<IActionResult> PutDesignAsync(int id, [FromBody] DesignRequest request)
    {
        var design = _mapper.Map<DesignRequest, Design>(request);
        var result = await _designDomain.UpdateDesignAsync(id, design);
        return Ok(result);
    }
    /// DELETE: api/v1/designs/{id}
    /// <summary>
    /// Delete a Design by Id
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDesignAsync(int id)
    {
        var result = await _designDomain.DeleteAsync(id);
        return Ok(result);
    }
}
   