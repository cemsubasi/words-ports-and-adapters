using Domain.Comment.Port;
using Domain.Comment.UseCase;

namespace Domain.Comment;

public class CreateCommentUseCaseHandler(CommentPort commentPort) {
  private readonly CommentPort commentPort = commentPort;

  public async Task Handle(CreateComment useCase, CancellationToken cancellationToken) {
    await this.commentPort.CreateAsync(useCase, cancellationToken);
  }
}
