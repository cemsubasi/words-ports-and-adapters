using Domain.Post.Port;
using Domain.Post.UseCase;

namespace Domain.Post;

public class CreatePostUseCaseHandler(PostPort postPort) {
  private readonly PostPort postPort = postPort;

  public async Task Handle(CreatePost useCase, CancellationToken cancellationToken) {
    await this.postPort.CreateAsync(useCase, cancellationToken);
  }
}
