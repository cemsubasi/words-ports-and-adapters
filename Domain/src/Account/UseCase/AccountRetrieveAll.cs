namespace Domain.Account.UseCase;

public class AccountRetrieveAll {
  public Guid Id { get; private set; }

  private AccountRetrieveAll(Guid id) {
    this.Id = id;
  }

  public static AccountRetrieveAll Build(Guid id) {
    return new AccountRetrieveAll(id);
  }
}
