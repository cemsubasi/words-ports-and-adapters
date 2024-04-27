using Domain.Category.Port;
using Domain.Comment.Entity;
using Domain.Comment.UseCase;
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

  public async Task AddComment(CreateComment useCase, CancellationToken cancellationToken) {
    var post = await this.context.Posts
      .Where(x => x.Id == useCase.PostId)
      .Include(x => x.Comments)
      .SingleOrDefaultAsync(cancellationToken);

    PostNotFoundException.ThrowIfNull(post);

    var commentEntity = CommentEntity.Create(
      id: Guid.NewGuid(),
      postId: useCase.PostId,
      parentCommentId: useCase.ParentCommentId,
      comment: useCase.Comment,
      createdBy: useCase.AccountId
    );

    var comment = post.AddComment(commentEntity);

    _ = await this.context.Comments.AddAsync(comment, cancellationToken);
    var result = await this.context.SaveChangesAsync(cancellationToken);

    if (result.Equals(0)) {
      throw new DbUpdateException();
    }
  }

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

  public async Task<CommentEntity[]> RetrieveCommentsAsync(RetrieveAllComments useCase, CancellationToken cancellationToken) {
    var entities = await this.context.Comments
      .Where(x => x.ParentCommentId == useCase.ParentCommentId)
      .Include(x => x.ParentComment)
      .Include(x => x.SubComments)
      .Skip(useCase.Page * useCase.Size)
      .Take(useCase.Size)
      .ToArrayAsync(cancellationToken);

    return entities;
  }

  public Task<CommentEntity> RetrieveCommentsAsync(RetrieveComment useCase, CancellationToken cancellationToken) => throw new NotImplementedException();
}
