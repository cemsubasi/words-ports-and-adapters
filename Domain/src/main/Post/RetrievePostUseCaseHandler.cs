using Domain.Common;
using Domain.Post.Entity;
using Domain.Post.Port;
using Domain.Post.UseCase;

namespace Domain.Post;

public class RetrievePostUseCaseHandler {
  private readonly PostPort postPort;

  public RetrievePostUseCaseHandler(PostPort postPort) => this.postPort = postPort;

  public async Task<PostEntity> Handle(RetrievePost useCase, CancellationToken cancellationToken) {
    var result = await this.postPort.RetrieveAsync(useCase, cancellationToken);

    return result;
  }

  public async Task<PostEntity[]> Handle(DataRequest useCase, CancellationToken cancellationToken) {
    var result = await this.postPort.RetrieveAllAsync(useCase, cancellationToken);

    return result;
  }
}
