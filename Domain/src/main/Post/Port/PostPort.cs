using Domain.Common;
using Domain.Post.Entity;
using Domain.Post.UseCase;

namespace Domain.Post.Port;

public interface PostPort {
  Task CreateAsync(CreatePost useCase, CancellationToken cancellationToken);
  Task<PostEntity> RetrieveAsync(RetrievePost useCase, CancellationToken cancellationToken);
  Task<PostEntity[]> RetrieveAllAsync(DataRequest useCase, CancellationToken cancellationToken);
}
