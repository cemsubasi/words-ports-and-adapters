using Moq;
using Xunit;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Post.Entity;
using Domain.Post.UseCase;
using Domain.Category.Port;
using Infra.Post.Adapter;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Domain.Category.Entity;
using System.Linq.Expressions;

namespace Tests.Infra.Post.Adapter;
public class PostAdapterTest {
  private readonly Mock<MainDbContext> _mockContext;
  private readonly Mock<CategoryPort> _mockCategoryPort;
  private readonly PostAdapter _postAdapter;
  private readonly Mock<DbSet<PostEntity>> _mockSet;

  public PostAdapterTest() {
    this._mockContext = new Mock<MainDbContext>();
    this._mockCategoryPort = new Mock<CategoryPort>();
    this._mockSet = new Mock<DbSet<PostEntity>>();

    this._mockContext.Setup(m => m.Posts).Returns(this._mockSet.Object);

    this._postAdapter = new PostAdapter(this._mockContext.Object, this._mockCategoryPort.Object);
  }

  [Fact]
  public async Task CreateAsync_ShouldCreateAndSavePostEntity_WhenCalledWithValidData() {
    // Arrange
    var fakeCategory = CategoryEntity.Create(Guid.NewGuid(), "Sample Category");
    this._mockCategoryPort.Setup(x => x.FindOrCreateCategoryAsync("Sample Category", It.IsAny<CancellationToken>()))
                     .ReturnsAsync(fakeCategory);

    var useCase = CreatePost.Build(
      accountId: Guid.NewGuid(),
      categoryId: fakeCategory.Id,
      categoryName: "Sample Category",
      url: "https://sample.com",
      header: "Sample Header",
      body: "Sample Body",
      thumbnail: "https://sample.com/thumbnail.jpg",
      isFeatured: true
    );

    // Act
    await this._postAdapter.CreateAsync(useCase, CancellationToken.None);

    // Assert
    this._mockSet.Verify(m => m.AddAsync(It.Is<PostEntity>(p =>
        p.AccountId == useCase.AccountId &&
        p.CategoryId == fakeCategory.Id &&
        p.Thumbnail == useCase.Thumbnail &&
        p.Url == useCase.Url &&
        p.Header == useCase.Header &&
        p.Body == useCase.Body &&
        p.IsFeatured == useCase.IsFeatured
    ), It.IsAny<CancellationToken>()), Times.Once);

    this._mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
  }
}