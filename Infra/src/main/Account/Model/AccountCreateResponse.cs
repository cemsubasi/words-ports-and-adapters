using Domain.Account.Entity;

namespace Infra.Controllers.Account;

public class AccountCreateResponse {
  public Guid Id { get; private set; }
  public string Name { get; private set; }
  public string Email { get; private set; }

  private AccountCreateResponse(Guid id, string name, string email) {
    this.Id = id;
    this.Name = name;
    this.Email = email;
  }

  public static AccountCreateResponse From(AccountEntity accountEntity) {
    return new AccountCreateResponse(accountEntity.Id, accountEntity.Name, accountEntity.Email);
  }
}
