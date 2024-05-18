namespace Domain.Category.UseCase;

public class CategoryDelete {
  public Guid Id { get; private set; }

  public Guid AccountId { get; private set; }

  private CategoryDelete(Guid id, Guid accountId) {
    this.AccountId = accountId;
    this.Id = id;
  }

  public static CategoryDelete Build(Guid id, Guid accountId) {
    return new CategoryDelete(id, accountId);
  }
}
