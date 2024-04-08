using Domain.Common;
using Domain.Post.UseCase;

namespace Infra.Post.Model;

public class RetrieveAllPostsRequest(int page, int size) : DataRequest(page, size) {
  public RetrieveAllPosts ToUseCase(Guid accountId) {
    return RetrieveAllPosts.Build(accountId, this.Page, this.Size);
  }
}
