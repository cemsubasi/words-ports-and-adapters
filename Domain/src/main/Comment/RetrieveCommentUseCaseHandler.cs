using Domain.Comment.Entity;
using Domain.Comment.Port;
using Domain.Comment.UseCase;
using Domain.Common;

namespace Domain.Comment;

public class RetrieveCommentUseCaseHandler {
  readonly CommentPort commentPort;

  public RetrieveCommentUseCaseHandler(CommentPort commentPort) {
    this.commentPort = commentPort;
  }

  public async Task<CommentEntity> HandleAsync(RetrieveComment useCase, CancellationToken cancellationToken) {
    return await commentPort.RetrieveAsync(useCase, cancellationToken);
  }

  public async Task<CommentEntity[]> HandleAllAsync(DataRequest useCase, CancellationToken cancellationToken) {
    return await commentPort.RetrieveAllAsync(useCase, cancellationToken);
  }
}
