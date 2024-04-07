using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Post.UseCase;

public class RetrieveAllPosts : DataRequest {
  [JsonIgnore]
  public Guid AccountId { get; private set; }

  public RetrieveAllPosts(Guid accountId, int page, int size) : base(page, size) {
    this.AccountId = accountId;
  }

  public RetrieveAllPosts Build(Guid accountId) {
    return new RetrieveAllPosts(accountId, this.Page, this.Size);
  }
}
