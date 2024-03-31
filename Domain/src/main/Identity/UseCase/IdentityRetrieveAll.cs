namespace Domain.Identity.UseCase;

public class IdentityRetrieveAll {
  public Guid Id { get; private set; }

  private IdentityRetrieveAll(Guid id) {
    this.Id = id;
  }

  public static IdentityRetrieveAll Build(Guid id) {
    return new IdentityRetrieveAll(id);
  }
}
