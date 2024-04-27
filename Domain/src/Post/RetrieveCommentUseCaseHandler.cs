using Domain.Comment.Entity;
using Domain.Comment.UseCase;
using Domain.Post.Port;

namespace Domain.Comment;

public class RetrieveCommentUseCaseHandler(PostPort postPort) {
  private readonly PostPort postPort = postPort;
  public async Task<CommentEntity> Handle(RetrieveComment useCase, CancellationToken cancellationToken) {
    return await this.postPort.RetrieveCommentsAsync(useCase, cancellationToken);
  }

  public async Task<CommentEntity[]> Handle(RetrieveAllComments useCase, CancellationToken cancellationToken) {
    return await this.postPort.RetrieveCommentsAsync(useCase, cancellationToken);
  }
}