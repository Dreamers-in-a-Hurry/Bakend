using System.Net;
using AutoMapper;
using Fitshirt.Api.Dtos.Categories;
using Fitshirt.Api.Errors;
using Fitshirt.Domain.Exceptions;
using Fitshirt.Infrastructure.Models.Posts.Entities;
using Fitshirt.Infrastructure.Repositories.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Fitshirt.Api.Controllers;

[Route("api/v1/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }
    /// GET: api/v1/categories
    /// <summary>
    /// Get a List of All Categories.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetCategoriesAsync()
    {
        var data = await _categoryRepository.GetAllAsync();
        
        if (data.Count == 0)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NoEntitiesFoundException(nameof(Category)));

            return NotFound(errorResponse);
        }
        
        var result = _mapper.Map<List<CategoryResponse>>(data);
        
        return Ok(result);
    }
    /// GET: api/v1/categories/{id}
    /// <summary>
    /// Get a List of Categories by Id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDesignByIdAsync(int id)
    {
        var data = await _categoryRepository.GetByIdAsync(id);
        if (data == null)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NotFoundEntityIdException(nameof(Category), id));

            return NotFound(errorResponse);
        }
        
        var designResponse = _mapper.Map<Category,CategoryResponse>(data);
        
        return Ok(designResponse);
    }
}