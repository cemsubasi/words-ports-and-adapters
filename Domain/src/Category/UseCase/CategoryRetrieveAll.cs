namespace Domain.Category.UseCase;

public class CategoryRetrieveAll {
  public Guid AccountId { get; set; }
  public int Page { get; set; }
  public int Size { get; set; }
}
