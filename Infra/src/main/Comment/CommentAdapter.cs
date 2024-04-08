using Domain.Comment.Entity;
using Domain.Comment.Port;
using Domain.Comment.UseCase;
using Infra.Comment.Model;
using Infra.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infra.Comment.Adapter;

public class CommentAdapter(MainDbContext context) : CommentPort {
  readonly MainDbContext context = context;

  public async Task CreateAsync(CreateComment useCase, CancellationToken cancellationToken) {
    var entity = CommentEntity.Create(
      id: Guid.NewGuid(),
      postId: useCase.PostId,
      parentCommentId: useCase.ParentCommentId,
      comment: useCase.Comment
    );

    if (entity is null) {
      throw new ArgumentNullException();
    }

    _ = await this.context.Comments.AddAsync(entity, cancellationToken);
    _ = await this.context.SaveChangesAsync(cancellationToken);
  }

  public async Task<CommentEntity[]> RetrieveAsync(RetrieveAllComments useCase, CancellationToken cancellationToken) {
    var entities = await this.context.Comments
      .Where(x => x.ParentCommentId == useCase.ParentCommentId)
      .Include(x => x.ParentComment)
      .Include(x => x.SubComments)
      .Skip(useCase.Page * useCase.Size)
      .Take(useCase.Size)
      .ToArrayAsync(cancellationToken);

    return entities;
  }

  public Task<CommentEntity> RetrieveAsync(RetrieveComment useCase, CancellationToken cancellationToken) => throw new NotImplementedException();
}
