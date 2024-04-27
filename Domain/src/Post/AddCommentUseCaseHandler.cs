using Domain.Comment.UseCase;
using Domain.Post.Port;

namespace Domain.Comment;

public class AddCommentUseCaseHandler(PostPort postPort) {
  private readonly PostPort postPort = postPort;

  public async Task Handle(CreateComment useCase, CancellationToken cancellationToken) {
    await this.postPort.AddComment(useCase, cancellationToken);
  }
}
