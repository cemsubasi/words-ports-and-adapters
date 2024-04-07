using Domain.Common;
using Domain.Post.UseCase;

namespace Infra.Post.Model;

public class RetrieveAllPostsRequest : DataRequest {
  public RetrieveAllPostsRequest(int page, int size) : base(page, size) {
  }

  public RetrieveAllPosts ToUseCase(Guid accountId) {
    return new RetrieveAllPosts(accountId, this.Page, this.Size);
  }
}
