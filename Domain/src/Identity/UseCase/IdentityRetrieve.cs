namespace Domain.Identity.UseCase;

public class IdentityRetrieve {
  public Guid Id { get; private set; }

  private IdentityRetrieve(Guid id) {
    this.Id = id;
  }

  public static IdentityRetrieve Build(Guid id) {
    return new IdentityRetrieve(id);
  }
}
