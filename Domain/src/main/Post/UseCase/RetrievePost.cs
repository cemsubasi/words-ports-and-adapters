using Domain.Comment.Entity;

namespace Domain.Post.UseCase;

public class RetrievePost {
  public Guid Id { get; private set; }
  public Guid AccountId { get; private set; }

  private RetrievePost(Guid id, Guid accountId) {
    this.Id = id;
    this.AccountId = accountId;
  }

  public static RetrievePost Build(Guid id, Guid accountId) {
    return new RetrievePost(id, accountId);
  }
}
