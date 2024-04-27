using System.Text.Json.Serialization;
using Domain.Common;

namespace Domain.Post.UseCase;

public class RetrieveAllPosts : DataRequest {
  [JsonIgnore]
  public Guid AccountId { get; private set; }

  private RetrieveAllPosts(Guid accountId, int page, int size) : base(page, size) {
    this.AccountId = accountId;
  }

  public static RetrieveAllPosts Build(Guid accountId, int page, int size) {
    return new RetrieveAllPosts(accountId, page, size);
  }
}
