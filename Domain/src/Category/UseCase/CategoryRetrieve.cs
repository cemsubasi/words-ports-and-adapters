namespace Domain.Category.UseCase;

public class CategoryRetrieve {
  public Guid Id { get; set; }
  public Guid AccountId { get; set; }

  public static CategoryRetrieve Build(Guid id, Guid accountId) {
    return new CategoryRetrieve {
      Id = id,
      AccountId = accountId,
    };
  }
}
