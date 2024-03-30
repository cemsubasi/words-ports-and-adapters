namespace Domain.SuperAccount.UseCase;

public class SuperAccountRetrieve {
  public Guid Id { get; private set; }

  private SuperAccountRetrieve(Guid id) {
    this.Id = id;
  }

  public static SuperAccountRetrieve Build(Guid id) {
    return new SuperAccountRetrieve(id);
  }
}
