using Domain.Comment.Entity;
using Domain.Comment.UseCase;
using Domain.Common;

namespace Domain.Comment.Port;

public interface CommentPort {
  Task CreateAsync(CreateComment useCase, CancellationToken cancellationToken);
  Task<CommentEntity> RetrieveAsync(RetrieveComment useCase, CancellationToken cancellationToken);
  Task<CommentEntity[]> RetrieveAllAsync(DataRequest useCase, CancellationToken cancellationToken);
  /* Task UpdateAsync(UpdateComment useCase, CancellationToken cancellationToken); */
  /* Task DeleteAsync(Guid id, CancellationToken cancellationToken); */
}
