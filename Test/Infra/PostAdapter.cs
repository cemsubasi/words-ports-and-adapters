using System.Threading;
using System.Threading.Tasks;
using Domain.Post.Entity;
using Domain.Post.UseCase;
using Infra.Context;
using Infra.Post.Adapter;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Test;

public class PostTests {
  private readonly Mock<MainDbContext> _mockContext;
  private readonly PostAdapter _service;
  private readonly CancellationToken _cancellationToken = default;

  public PostTests() {
    _mockContext = new Mock<MainDbContext>();
    _service = new PostAdapter(_mockContext.Object);
  }

  [Fact]
  public async Task CreateAsync_ShouldAddPost_WhenCalled() {
    // Arrange
    var useCase = CreatePost.Build(Guid.NewGuid(), null, "test", "http://test.url", "Test Header", "Test Body", "Negro", true);
    var mockSet = new Mock<DbSet<PostEntity>>();
    _mockContext.Setup(m => m.Posts).Returns(mockSet.Object);

    // Eğer ValidateAndSetCategoryAsync içerisinde CategoryId set edilirse, bu kısmı da mocklamanız gerekebilir.

    // Act
    await _service.CreateAsync(useCase, _cancellationToken);

    // Assert
    mockSet.Verify(m => m.AddAsync(It.IsAny<PostEntity>(), _cancellationToken), Times.Once);
    _mockContext.Verify(m => m.SaveChangesAsync(_cancellationToken), Times.Once);
  }
}
