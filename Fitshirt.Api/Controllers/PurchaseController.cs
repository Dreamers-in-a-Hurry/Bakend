using AutoMapper;
using Fitshirt.Api.Dtos.Purchases;
using Fitshirt.Api.Errors;
using Fitshirt.Domain.Exceptions;
using Fitshirt.Domain.Features.Purchases;
using Fitshirt.Infrastructure.Models.Purchases;
using Fitshirt.Infrastructure.Repositories.Purchases;
using Microsoft.AspNetCore.Mvc;

namespace Fitshirt.Api.Controllers;

[Route("api/v1/purchases")]
[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseDomain _purchaseDomain;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IMapper _mapper;

    public PurchaseController(IPurchaseDomain purchaseDomain, IPurchaseRepository purchaseRepository, IMapper mapper)
    {
        _purchaseDomain = purchaseDomain;
        _purchaseRepository = purchaseRepository;
        _mapper = mapper;
    }

    /// GET: api/v1/purchases
    /// <summary>
    /// Get a List of All Purchases.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetPurchasesAsync()
    {
        var data = await _purchaseRepository.GetAllAsync();
        
        if (data.Count == 0)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NoEntitiesFoundException(nameof(Purchase)));

            return NotFound(errorResponse);
        }

        var result = _mapper.Map<List<PurchaseResponse>>(data);

        return Ok(result);
    }

    /// GET: api/v1/purchases/{id}
    /// <summary>
    /// Get a List of Purchases by Id.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPurchaseByIdAsync(int id)
    {
        var data = await _purchaseRepository.GetByIdAsync(id);

        if (data == null)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NotFoundEntityIdException(nameof(Purchase), id));

            return NotFound(errorResponse);
        }

        var purchaseResponse = _mapper.Map<Purchase, PurchaseResponse>(data);
        return Ok(purchaseResponse);
    }
    
    /// GET: api/v1/purchases
    /// <summary>
    /// Get a Purchase by UserId.
    /// </summary>
    [HttpGet]
    [Route("search-by-user")]
    public async Task<IActionResult> GetPurchasesByUserIdAsync(int userId)
    {
        var data = await _purchaseRepository.GetPurchasesByUserId(userId);

        if (data.Count == 0)
        {
            var errorResponse =
                CodeErrorResponseFactory.CreateCodeErrorResponse(new NoEntitiesFoundException(nameof(Purchase)));

            return NotFound(errorResponse);
        }

        var result = _mapper.Map<List<PurchaseResponse>>(data);
        return Ok(result);
    }

    /// POST: api/v1/purchases
    /// <summary>
    /// Buy a Shirt.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/v1/purchases
    ///     {
    ///        "userId": 1,
    ///        "items": [
    ///        {
    ///          "postId": "The post Id",
    ///          "sizeId": "The size Id",
    ///          "quantity": 1
    ///        }
    /// ]
    ///     }
    ///
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> PostPurchaseAsync([FromBody] PurchaseRequest request)
    {
        var purchase = _mapper.Map<PurchaseRequest, Purchase>(request);
        var result = await _purchaseDomain.AddAsync(purchase);
        return StatusCode(StatusCodes.Status201Created, result);
    }
}