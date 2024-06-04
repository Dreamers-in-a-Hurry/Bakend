using System.ComponentModel;
using AutoMapper;
using Fitshirt.Api.Dtos.Posts;
using Fitshirt.Api.Dtos.PostsSizes;
using Fitshirt.Api.Errors;
using Fitshirt.Domain.Exceptions;
using Fitshirt.Domain.Features.Posts;
using Fitshirt.Infrastructure.Models.Posts;
using Fitshirt.Infrastructure.Repositories.Posts;
using Microsoft.AspNetCore.Mvc;

namespace Fitshirt.Api.Controllers;

[Route("api/v1/posts")]
[ApiController]
public class PostController : ControllerBase
{
    private readonly IPostDomain _postDomain;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public PostController(IPostDomain postDomain, IPostRepository postRepository, IMapper mapper)
    {
        _postDomain = postDomain;
        _postRepository = postRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [Description("Get posts in view mode for catalogue")]
    public async Task<ActionResult> GetPostsAsync()
    {
        var data = await _postRepository.GetAllAsync();

        if (data.Count == 0)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NoEntitiesFoundException(nameof(Post)));

            return NotFound(errorResponse);
        }
        
        var result = _mapper.Map<List<ShirtVm>>(data);
        
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    [Description("Get posts in details for catalogue description and for published details")]
    public async Task<IActionResult> GetPostByIdAsync(int id)
    {
        var data = await _postRepository.GetByIdAsync(id);
        if (data == null)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NotFoundEntityIdException(nameof(Post), id));

            return NotFound(errorResponse);
        }

        var sizesResponse = _mapper.Map<List<PostSizeResponse>>(data.PostSizes);
        var postResponse = _mapper.Map<Post, PostResponse>(data);
        postResponse.Sizes = sizesResponse;

        return Ok(postResponse);
        
    }

    [HttpGet]
    [Route("SearchByUser")]
    [Description("Get posts in view mode published by an user")]
    public async Task<IActionResult> GetPostsByUserIdAsync(int userId)
    {
        var data = await _postRepository.GetPostsByUserId(userId);

        if (data.Count == 0)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NotFoundEntityIdException(nameof(User), userId));

            return NotFound(errorResponse);
        }
        
        var result = _mapper.Map<List<ShirtVm>>(data);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("filter-shirts")]
    [Description("Get posts in view mode by filters")]
    public async Task<IActionResult> GetPostsByFilterAsync(int? categoryId, int? colorId)
    {
        var data = await _postRepository.SearchByFiltersAsync(categoryId, colorId);
        
        if (data.Count == 0)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NoEntitiesFoundException(nameof(Post)));

            return NotFound(errorResponse);
        }
        
        var result = _mapper.Map<List<ShirtVm>>(data);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> PostPostAsync([FromBody] PostRequest request)
    {
        var post = _mapper.Map<PostRequest, Post>(request);
        var result = await _postDomain.AddPostAsync(post, request.SizeIds);
        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut]
    public async Task<IActionResult> PutPostAsync(int id, [FromBody] PostRequest request)
    {
        var post = _mapper.Map<PostRequest, Post>(request);
        var result = await _postDomain.UpdatePostAsync(id, post, request.SizeIds);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePostAsync(int id)
    {
        var result = await _postDomain.DeleteAsync(id);
        return Ok(result);
    }
    
}