using Domain.Post.Port;
using Domain.Post.UseCase;

namespace Domain.Post;

public class CreatePostUseCaseHandler {
  private readonly PostPort postPort;

  public CreatePostUseCaseHandler(PostPort postPort) {
    this.postPort = postPort;
  }

  public async Task Handle(CreatePost useCase, CancellationToken cancellationToken) {
    await this.postPort.CreateAsync(useCase, cancellationToken);
  }
}
