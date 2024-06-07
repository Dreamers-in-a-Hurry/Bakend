
using AutoMapper;
using Fitshirt.Api.Controllers;
using Fitshirt.Api.Dtos.Colors;
using Fitshirt.Api.Dtos.Designs;
using Fitshirt.Api.Dtos.Posts;
using Fitshirt.Api.Dtos.Shields;
using Fitshirt.Api.Dtos.Users;
using Fitshirt.Domain.Features.Designs;
using Fitshirt.Infrastructure.Models.Designs;
using Fitshirt.Infrastructure.Repositories.Designs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace Fitshirt.Api.Tests.Controllers;


public class DesignControllerUnitTest
{
    
    [Fact]
    public async Task GetDesignsAsync_ReturnSuccess()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var designRepositoryMock = Substitute.For<IDesignRepository>();
        var designDomainMock = Substitute.For<IDesignDomain>();
        var designController = new DesignController(designDomainMock, designRepositoryMock, mapperMock);

        var list = new List<Design>
        {
            new Design
            {
                UserId = 1,
                PrimaryColorId = 1,
                SecondaryColorId = 1,
                TertiaryColorId = 1,
                ShieldId = 1
            }
        };

        var returnList = new List<ShirtVm>
        {
            new ShirtVm
            {
                // Propiedades de ShirtVm según tu implementación
            }
        };

        designRepositoryMock.GetAllAsync().Returns(Task.FromResult<IReadOnlyList<Design>>(list));
        mapperMock.Map<List<ShirtVm>>(list).Returns(returnList);

        // Act
        var result = await designController.GetDesignsAsync();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ShirtVm>>(okResult.Value);
        Assert.Equal(returnList.Count, returnValue.Count);
    }
    [Fact]
    public async Task GetDesignByIdAsync_ReturnSuccess()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var designRepositoryMock = Substitute.For<IDesignRepository>();
        var designDomainMock = Substitute.For<IDesignDomain>();
        var designController = new DesignController(designDomainMock, designRepositoryMock, mapperMock);

        var design = new Design
        {
            UserId = 1,
            PrimaryColorId = 1,
            SecondaryColorId = 1,
            TertiaryColorId = 1,
            ShieldId = 1
        };

        var designResponse = new DesignResponse
        {
            // Propiedades de DesignResponse según tu implementación
        };

        designRepositoryMock.GetByIdAsync(1).Returns(Task.FromResult(design));
        mapperMock.Map<Design, DesignResponse>(design).Returns(designResponse);

        // Act
        var result = await designController.GetDesignByIdAsync(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<DesignResponse>(okResult.Value);
        Assert.Equal(designResponse, returnValue);
    }
    
    [Fact]
    public async Task GetDesignByIdAsync_ReturnNotFound()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var designRepositoryMock = Substitute.For<IDesignRepository>();
        var designDomainMock = Substitute.For<IDesignDomain>();
        var designController = new DesignController(designDomainMock, designRepositoryMock, mapperMock);

        designRepositoryMock.GetByIdAsync(1).Returns(Task.FromResult<Design>(null));

        // Act
        var result = await designController.GetDesignByIdAsync(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
    /* [Fact]
    public async Task GetDesignByUserIdAsync_ReturnSuccess()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var designRepositoryMock = Substitute.For<IDesignRepository>();
        var designDomainMock = Substitute.For<IDesignDomain>();
        var designController = new DesignController(designDomainMock, designRepositoryMock, mapperMock);

        var list = new List<Design>
        {
            new Design
            {
                UserId = 1,
                PrimaryColorId = 1,
                SecondaryColorId = 1,
                TertiaryColorId = 1,
                ShieldId = 1
            }
        };

        var returnList = new List<ShirtVm>
        {
            new ShirtVm
            {
                // Propiedades de ShirtVm según tu implementación
            }
        };

        designRepositoryMock.GetDesignsByUserId(1).Returns(Task.FromResult<IReadOnlyList<Design>>(list));
        mapperMock.Map<List<ShirtVm>>(list).Returns(returnList);

        // Act
        var result = await designController.GetDesignByUserIdAsync(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<List<ShirtVm>>(okResult.Value);
        Assert.Equal(returnList.Count, returnValue.Count);
    }
    
    [Fact]
    public async Task GetDesignByUserIdAsync_ReturnNotFound()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var designRepositoryMock = Substitute.For<IDesignRepository>();
        var designDomainMock = Substitute.For<IDesignDomain>();
        var designController = new DesignController(designDomainMock, designRepositoryMock, mapperMock);

        designRepositoryMock.GetDesignsByUserId(1).Returns(Task.FromResult<IReadOnlyList<Design>>(new List<Design>()));

        // Act
        var result = await designController.GetDesignByUserIdAsync(1);

        // Assert
        Assert.IsType<NotFoundObjectResult>(result);
    }
    [Fact]
    public async Task PostDesignAsync_ReturnCreated()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var designRepositoryMock = Substitute.For<IDesignRepository>();
        var designDomainMock = Substitute.For<IDesignDomain>();
        var designController = new DesignController(designDomainMock, designRepositoryMock, mapperMock);

        var designRequest = new DesignRequest
        {
            // Propiedades de DesignRequest según tu implementación
        };

        var design = new Design
        {
            // Propiedades de Design según tu implementación
        };

        designDomainMock.AddDesignAsync(design).Returns(Task.FromResult(design));
        mapperMock.Map<DesignRequest, Design>(designRequest).Returns(design);

        // Act
        var result = await designController.PostDesignAsync(designRequest);

        // Assert
        var createdResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(StatusCodes.Status201Created, createdResult.StatusCode);
        var returnValue = Assert.IsType<Design>(createdResult.Value);
        Assert.Equal(design, returnValue);
    }
    
    [Fact]
    public async Task PutDesignAsync_ReturnOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var designRepositoryMock = Substitute.For<IDesignRepository>();
        var designDomainMock = Substitute.For<IDesignDomain>();
        var designController = new DesignController(designDomainMock, designRepositoryMock, mapperMock);

        var designRequest = new DesignRequest
        {
            // Propiedades de DesignRequest según tu implementación
        };

        var design = new Design
        {
            // Propiedades de Design según tu implementación
        };

        var updatedDesign = new Design
        {
            // Propiedades de Design según tu implementación
        };

        designDomainMock.UpdateDesignAsync(1, design).Returns(Task.FromResult(updatedDesign));
        mapperMock.Map<DesignRequest, Design>(designRequest).Returns(design);

        // Act
        var result = await designController.PutDesignAsync(1, designRequest);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Design>(okResult.Value);
        Assert.Equal(updatedDesign, returnValue);
    }
    [Fact]
    public async Task DeleteDesignAsync_ReturnOk()
    {
        // Arrange
        var mapperMock = Substitute.For<IMapper>();
        var designRepositoryMock = Substitute.For<IDesignRepository>();
        var designDomainMock = Substitute.For<IDesignDomain>();
        var designController = new DesignController(designDomainMock, designRepositoryMock, mapperMock);

        var design = new Design
        {
            // Propiedades de Design según tu implementación
        };

        designDomainMock.DeleteAsync(1).Returns(Task.FromResult(design));

        // Act
        var result = await designController.DeleteDesignAsync(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<Design>(okResult.Value);
        Assert.Equal(design, returnValue);
    }*/
}