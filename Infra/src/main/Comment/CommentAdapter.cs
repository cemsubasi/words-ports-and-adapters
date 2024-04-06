using Domain.Comment.Entity;
using Domain.Comment.Port;
using Domain.Comment.UseCase;
using Domain.Common;
using Infra.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Infra.Comment.Adapter;

public class CommentAdapter : CommentPort {
  readonly MainDbContext context;

  public CommentAdapter(MainDbContext context) => this.context = context;

  public async Task CreateAsync(CreateComment useCase, CancellationToken cancellationToken) {
    var entity = useCase.Adapt<CommentEntity>();

    if (entity is null) {
      throw new ArgumentNullException();
    }

    _ = await this.context.Comments.AddAsync(entity, cancellationToken);
    _ = await this.context.SaveChangesAsync(cancellationToken);
  }

  public async Task<CommentEntity[]> RetrieveAllAsync(DataRequest useCase, CancellationToken cancellationToken) {
    var entities = await this.context.Comments.ToArrayAsync(cancellationToken);
    return entities;
  }
  public Task<CommentEntity> RetrieveAsync(RetrieveComment useCase, CancellationToken cancellationToken) => throw new NotImplementedException();
}
