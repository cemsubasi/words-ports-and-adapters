namespace Domain.SuperAccount.UseCase;

public class SuperAccountRetrieveAll {
  public Guid Id { get; private set; }

  private SuperAccountRetrieveAll(Guid id) {
    this.Id = id;
  }

  public static SuperAccountRetrieveAll Build(Guid id) {
    return new SuperAccountRetrieveAll(id);
  }
}
