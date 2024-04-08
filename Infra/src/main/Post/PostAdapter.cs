using Domain.Category.Port;
using Domain.Post.Entity;
using Domain.Post.Port;
using Domain.Post.UseCase;
using Infra.Account;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Post.Adapter;

public class PostAdapter(MainDbContext context, CategoryPort categoryPort) : PostPort {
  private readonly MainDbContext context = context;
  private readonly CategoryPort categoryPort = categoryPort;

  public async Task CreateAsync(CreatePost useCase, CancellationToken cancellationToken) {
    var category = await this.categoryPort.FindOrCreateCategoryAsync(useCase.CategoryName, cancellationToken);

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
      .ThenInclude(x => x.SubComments)
      .Include(x => x.Category)
      .SingleOrDefaultAsync(cancellationToken);

    PostNotFoundException.ThrowIfNull(post);

    return post;
  }
}
