using System.ComponentModel;
using AutoMapper;
using Fitshirt.Api.Dtos.Designs;
using Fitshirt.Api.Dtos.DesignShields;
using Fitshirt.Api.Dtos.Posts;
using Fitshirt.Api.Dtos.PostsSizes;
using Fitshirt.Domain.Features.Designs;
using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Models.Posts;
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
    
    [HttpGet]
    [Description("Get designs in view mode for catalogue")]
    public async Task<ActionResult> GetDesignsAsync()
    {
        var data = await _designRepository.GetAllAsync();
        var result = _mapper.Map<List<ShirtVm>>(data);
        
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [Description("Get designs in details for catalogue description and for designs details")]
    public async Task<IActionResult> GetDesignByIdAsync(int id)
    {
        var data = await _designRepository.GetByIdAsync(id);
        if (data == null) return NotFound();
        
        var shieldResponse = _mapper.Map<List<DesignShieldResponse>>(data.DesignShields);
        var designResponse = _mapper.Map<Design,DesignResponse>(data);
        designResponse.NameTeam = shieldResponse;
        
        return Ok(designResponse);
    }

    [HttpGet]
    [Route("SearchByUser")]
    [Description("Get designs in view mode published by an user")]
    public async Task<IActionResult> GetDesignByUserIdAsync(int userId)
    {
        var data = await _designRepository.GetDesignsByUserId(userId);
        var result = _mapper.Map<List<ShirtVm>>(data);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostDesignAsync([FromBody] DesignRequest request)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var design = _mapper.Map<DesignRequest, Design>(request);
            var result = await _designDomain.AddDesignAsync(design, request.ShieldId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutDesignAsync(int id, [FromBody] DesignRequest request)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            var design = _mapper.Map<DesignRequest, Design>(request);

            var result = await _designDomain.UpdateDesignAsync(id, design, request.ShieldId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDesignAsync(int id)
    {
        var result = await _designDomain.DeleteAsync(id);

        return Ok(result);
    }
}
   