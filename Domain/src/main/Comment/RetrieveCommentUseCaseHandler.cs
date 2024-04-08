using Domain.Comment.Entity;
using Domain.Comment.Port;
using Domain.Comment.UseCase;

namespace Domain.Comment;

public class RetrieveCommentUseCaseHandler(CommentPort commentPort) {
  readonly CommentPort commentPort = commentPort;

  public async Task<CommentEntity> HandleAsync(RetrieveComment useCase, CancellationToken cancellationToken) {
    return await this.commentPort.RetrieveAsync(useCase, cancellationToken);
  }

  public async Task<CommentEntity[]> HandleAsync(RetrieveAllComments useCase, CancellationToken cancellationToken) {
    return await this.commentPort.RetrieveAsync(useCase, cancellationToken);
  }
}