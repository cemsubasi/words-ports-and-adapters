using Domain.Post.UseCase;

namespace Infra.Post.Model;

public class PostRetrieveRequest {
  /* public Guid Id { get; set; } */

  public static RetrievePost ToUseCase(Guid id, Guid accountId) {
    return RetrievePost.Build(id: id, accountId: accountId);
  }
}
