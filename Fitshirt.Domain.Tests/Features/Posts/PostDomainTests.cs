using Fitshirt.Domain.Exceptions;
using Fitshirt.Domain.Features.Posts;
using Fitshirt.Infrastructure.Models.Common.Entities;
using Fitshirt.Infrastructure.Models.Posts;
using Fitshirt.Infrastructure.Models.Posts.Entities;
using Fitshirt.Infrastructure.Models.Users;
using Fitshirt.Infrastructure.Repositories.Common.Entites;
using Fitshirt.Infrastructure.Repositories.Posts;
using Fitshirt.Infrastructure.Repositories.Users;
using Moq;

namespace Fitshirt.Domain.Tests.Features.Posts;

public class PostDomainTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
    private readonly Mock<IColorRepository> _colorRepositoryMock;
    private readonly Mock<ISizeRepository> _sizeRepositoryMock;
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly PostDomain _postDomain;
    
    public PostDomainTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _colorRepositoryMock = new Mock<IColorRepository>();
        _sizeRepositoryMock = new Mock<ISizeRepository>();
        _postRepositoryMock = new Mock<IPostRepository>();

        _postDomain = new PostDomain(
            _postRepositoryMock.Object,
            _userRepositoryMock.Object,
            _categoryRepositoryMock.Object,
            _colorRepositoryMock.Object,
            _sizeRepositoryMock.Object
        );
    }
    
    [Fact]
    public async Task AddPostAsync_ValidPost_ReturnsTrue()
    {
        // Arrange
        var post = new Post
        {
            UserId = 1,
            CategoryId = 1,
            ColorId = 1
        };
        var sizeIds = new List<int> { 1, 2 };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(post.UserId))
            .ReturnsAsync(new User { Id = post.UserId });
        _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(post.CategoryId))
            .ReturnsAsync(new Category { Id = post.CategoryId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(post.ColorId))
            .ReturnsAsync(new Color { Id = post.ColorId });
        _sizeRepositoryMock.Setup(repo => repo.GetSizesByIdsAsync(sizeIds))
            .ReturnsAsync(new List<Size>
        {
            new Size { Id = 1 },
            new Size { Id = 2 }
        });
        _postRepositoryMock.Setup(repo => repo.AddAsync(post)).ReturnsAsync(true);

        // Act
        var result = await _postDomain.AddPostAsync(post, sizeIds);

        // Assert
        Assert.True(result);

        _userRepositoryMock.Verify(repo => repo.GetByIdAsync(post.UserId), Times.Once);
        _categoryRepositoryMock.Verify(repo => repo.GetByIdAsync(post.CategoryId), Times.Once);
        _colorRepositoryMock.Verify(repo => repo.GetByIdAsync(post.ColorId), Times.Once);
        _sizeRepositoryMock.Verify(repo => repo.GetSizesByIdsAsync(sizeIds), Times.Once);
        _postRepositoryMock.Verify(repo => repo.AddAsync(post), Times.Once);
    }

    [Fact]
    public async Task AddPostAsync_InvalidUserId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        var notExistingUserId = 9999;
        var post = new Post
        {
            UserId = notExistingUserId,
        };
        var sizeIds = new List<int> { 1, 2 };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(post.UserId))
            .ReturnsAsync((User)null);
        
        // Act
        var result = async()=> await _postDomain.AddPostAsync(post, sizeIds);
        
        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("User", nameof(User));
        Assert.Equal(notExistingUserId, post.UserId);
    }
    
    [Fact]
    public async Task AddPostAsync_InvalidCategoryId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        var notExistingCategoryId = 9999;
        var post = new Post
        {
            CategoryId = notExistingCategoryId, 
        };
        var sizeIds = new List<int> { 1, 2 };

        _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(post.CategoryId))
            .ReturnsAsync((Category)null);
        
        // Act
        var result = async()=> await _postDomain.AddPostAsync(post, sizeIds);
        
        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Category", nameof(Category));
        Assert.Equal(notExistingCategoryId, post.CategoryId);
    }
    
    [Fact]
    public async Task AddPostAsync_InvalidColorId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        var notExistingColorId = 9999;
        var post = new Post
        {
            ColorId = notExistingColorId
        };
        var sizeIds = new List<int> { 1, 2 };

        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(post.ColorId))
            .ReturnsAsync((Color)null);
        
        // Act
        var result = async()=> await _postDomain.AddPostAsync(post, sizeIds);
        
        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Color", nameof(Color));
        Assert.Equal(notExistingColorId, post.ColorId);
    }
    
    [Fact]
    public async Task AddPostAsync_InvalidAnySizeId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        var notExistingSizeId = 9999;
        var post = new Post
        {
            UserId = 1,
            CategoryId = 1, 
            ColorId = 1
        };
        var sizeIds = new List<int> { 1, notExistingSizeId };

        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(post.UserId))
            .ReturnsAsync(new User { Id = post.UserId });
        _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(post.CategoryId))
            .ReturnsAsync(new Category {Id = post.CategoryId});
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(post.ColorId))
            .ReturnsAsync(new Color { Id = post.ColorId });
        _sizeRepositoryMock.Setup(repo => repo.GetSizesByIdsAsync(sizeIds))
            .ReturnsAsync(new List<Size>
            {
                new Size { Id = 1 }
            });
        
        // Act
        var result = async()=> await _postDomain.AddPostAsync(post, sizeIds);
        
        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundInListException<int>>(result);
        Assert.Equal("Size", nameof(Size));
        Assert.Equal("Id", nameof(Size.Id));
    }

    [Fact]
    public async Task DeleteAsync_ValidData_ReturnsTrue()
    {
        // Arrange
        var post = new Post()
        {
            Id = 1
        };

        _postRepositoryMock.Setup(repo => repo.GetByIdAsync(post.Id))
            .ReturnsAsync(post);
        _postRepositoryMock.Setup(repo => repo.DeleteAsync(post.Id))
            .ReturnsAsync(true);
        
        // Act
        var result = await _postDomain.DeleteAsync(post.Id);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_InvalidPostId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        var idNotFound = 999;
        var post = new Post()
        {
            Id = idNotFound
        };

        _postRepositoryMock.Setup(repo => repo.GetByIdAsync(post.Id))
            .ReturnsAsync((Post)null);
        
        // Act
        var result = async () => await _postDomain.DeleteAsync(post.Id);
        
        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Post", nameof(Post));
        Assert.Equal(idNotFound, post.Id);
    }

    [Fact]
    public async Task UpdatePostAsync_ValidData_ReturnsTrue()
    {
        // Arrange
        var post = new Post
        {
            Id = 1
        };
        var sizeIds = new List<int> { 1, 2 };

        _postRepositoryMock.Setup(repo => repo.GetByIdAsync(post.Id))
            .ReturnsAsync(post);
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(post.UserId))
            .ReturnsAsync(new User { Id = post.UserId });
        _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(post.CategoryId))
            .ReturnsAsync(new Category { Id = post.CategoryId });
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(post.ColorId))
            .ReturnsAsync(new Color { Id = post.ColorId });
        _sizeRepositoryMock.Setup(repo => repo.GetSizesByIdsAsync(sizeIds))
            .ReturnsAsync(new List<Size>
            {
                new Size { Id = 1 },
                new Size { Id = 2 }
            });
        _postRepositoryMock.Setup(repo => repo.UpdateAsync(post.Id, post))
            .ReturnsAsync(true);

        // Act
        var result = await _postDomain.UpdatePostAsync(post.Id, post, sizeIds);

        // Assert
        Assert.True(result);

        _userRepositoryMock.Verify(repo => repo.GetByIdAsync(post.UserId), Times.Once);
        _categoryRepositoryMock.Verify(repo => repo.GetByIdAsync(post.CategoryId), Times.Once);
        _colorRepositoryMock.Verify(repo => repo.GetByIdAsync(post.ColorId), Times.Once);
        _sizeRepositoryMock.Verify(repo => repo.GetSizesByIdsAsync(sizeIds), Times.Once);
        _postRepositoryMock.Verify(repo => repo.UpdateAsync(post.Id, post), Times.Once);
    }

    [Fact]
    public async Task UpdatePostAsync_InvalidPostId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        var idNotFound = 999;
        var post = new Post()
        {
            Id = idNotFound
        };
        var sizeIds = new List<int> { 1, 2 };

        _postRepositoryMock.Setup(repo => repo.GetByIdAsync(post.Id))
            .ReturnsAsync((Post)null);
        
        // Act
        var result = async () => await _postDomain.UpdatePostAsync(idNotFound, post, sizeIds);
        
        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Post", nameof(Post));
        Assert.Equal(idNotFound, post.Id);
    }
    
    [Fact]
    public async Task UpdatePostAsync_InvalidUserId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        var idNotFound = 999;
        var post = new Post()
        {
            UserId = idNotFound
        };
        var sizeIds = new List<int> { 1, 2 };

        _postRepositoryMock.Setup(repo => repo.GetByIdAsync(post.Id))
            .ReturnsAsync(new Post{Id = 1});
        _userRepositoryMock.Setup(repo => repo.GetByIdAsync(post.UserId))
            .ReturnsAsync((User)null);
        
        // Act
        var result = async () => await _postDomain.UpdatePostAsync(1, post, sizeIds);
        
        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("User", nameof(User));
        Assert.Equal(idNotFound, post.UserId);
    }
    
    [Fact]
    public async Task UpdatePostAsync_InvalidCategoryId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        var idNotFound = 999;
        var post = new Post()
        {
            CategoryId = idNotFound
        };
        var sizeIds = new List<int> { 1, 2 };

        _postRepositoryMock.Setup(repo => repo.GetByIdAsync(post.Id))
            .ReturnsAsync(new Post{Id = 1});
        _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(post.CategoryId))
            .ReturnsAsync((Category)null);
        
        // Act
        var result = async () => await _postDomain.UpdatePostAsync(1, post, sizeIds);
        
        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Category", nameof(Category));
        Assert.Equal(idNotFound, post.CategoryId);
    }
    
    [Fact]
    public async Task UpdatePostAsync_InvalidColorId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        var idNotFound = 999;
        var post = new Post()
        {
            ColorId = 999
        };
        var sizeIds = new List<int> { 1, 2 };

        _postRepositoryMock.Setup(repo => repo.GetByIdAsync(post.Id))
            .ReturnsAsync(new Post{Id = 1});
        _colorRepositoryMock.Setup(repo => repo.GetByIdAsync(post.ColorId))
            .ReturnsAsync((Color)null);
        
        // Act
        var result = async () => await _postDomain.UpdatePostAsync(1, post, sizeIds);
        
        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Color", nameof(Color));
        Assert.Equal(idNotFound, post.ColorId);
    }
    
    [Fact]
    public async Task UpdatePostAsync_InvalidAnySizeId_ThrowsNotFoundEntityIdException()
    {
        // Arrange
        var idNotFound = 999;
        var post = new Post();
        var sizeIds = new List<int> { 1, idNotFound };

        _postRepositoryMock.Setup(repo => repo.GetByIdAsync(post.Id))
            .ReturnsAsync(new Post{Id = 1});
        _sizeRepositoryMock.Setup(repo => repo.GetSizesByIdsAsync(sizeIds))
            .ReturnsAsync(new List<Size>
            {
                new Size { Id = 1 }
            });
        
        // Act
        var result = async () => await _postDomain.UpdatePostAsync(1, post, sizeIds);
        
        // Assert
        var exception = await Assert.ThrowsAsync<NotFoundEntityIdException>(result);
        Assert.Equal("Size", nameof(Size));
        Assert.Equal("Id", nameof(Size.Id));
    }
    
}