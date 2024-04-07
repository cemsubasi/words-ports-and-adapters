using Domain.Category.Entity;
using Domain.Common;
using Domain.Post.Entity;
using Domain.Post.Port;
using Domain.Post.UseCase;
using Infra.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infra.Post.Adapter;

public class PostAdapter : PostPort {
  private readonly MainDbContext context;
  public PostAdapter(MainDbContext context) {
    this.context = context;
  }

  public async Task CreateAsync(CreatePost useCase, CancellationToken cancellationToken) {
    var category = await FindOrCreateCategoryAsync(useCase.CategoryName, cancellationToken);

    var postEntity = PostEntity.Create(
      id: Guid.NewGuid(),
      accountId: useCase.AccountId,
      categoryId: category.Id,
      thumbnail: useCase.Thumbnail,
      url: useCase.Url,
      header: useCase.Header,
      body: useCase.Body,
      isFeatured: useCase.IsFeatured
    );

    await this.context.Posts.AddAsync(postEntity, cancellationToken);
    await this.context.SaveChangesAsync(cancellationToken);
  }

  private async Task<CategoryEntity> FindOrCreateCategoryAsync(string categoryName, CancellationToken cancellationToken) {
    var category = await this.context.Categories
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.Category == categoryName, cancellationToken);

    if (category is null) {
      category = CategoryEntity.Create(
        id: Guid.NewGuid(),
        category: categoryName);

      await this.context.Categories.AddAsync(category, cancellationToken);
      await this.context.SaveChangesAsync(cancellationToken);
    }

    return category;
  }

  public async Task<PostEntity[]> RetrieveAllAsync(RetrieveAllPosts useCase, CancellationToken cancellationToken) {
    var posts = await this.context.Posts
      .AsNoTrackingWithIdentityResolution()
      .Where(x => x.AccountId == useCase.AccountId)
      .Include(x => x.Comments)
      .ThenInclude(x => x.SubComments)
      .Include(x => x.Account)
      .Include(x => x.Category)
      .Skip(useCase.Page * useCase.Size)
      .Take(useCase.Size)
      .ToArrayAsync(cancellationToken);

    return posts;
  }

  public async Task<PostEntity> RetrieveAsync(RetrievePost useCase, CancellationToken cancellationToken) {
    var post = await this.context.Posts
      .Where(x => x.AccountId == useCase.AccountId && x.Id == useCase.Id)
      .Include(x => x.Comments)
      .SingleOrDefaultAsync(cancellationToken);

    return post;
  }
}
