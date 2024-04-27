namespace Domain.Account.UseCase;

public class AccountRetrieve {
  public Guid Id { get; private set; }

  private AccountRetrieve(Guid id) {
    this.Id = id;
  }

  public static AccountRetrieve Build(Guid id) {
    return new AccountRetrieve(id);
  }
}
