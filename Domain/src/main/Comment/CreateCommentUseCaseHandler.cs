using Domain.Comment.Port;
using Domain.Comment.UseCase;

namespace Domain.Comment;

public class CreateCommentUseCaseHandler {
  private readonly CommentPort commentPort;

  public CreateCommentUseCaseHandler(CommentPort commentPort) {
    this.commentPort = commentPort;
  }

  public async Task Handle(CreateComment useCase, CancellationToken cancellationToken) {
    await this.commentPort.CreateAsync(useCase, cancellationToken);
  }
}
