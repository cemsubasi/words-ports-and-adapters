using Domain.Account.Entity;

namespace Infra.Controllers.Account;

public class AccountRetrieveResponse {
  public Guid Id { get; private set; }

  public string Name { get; private set; }

  public string Email { get; private set; }

  public string Phone { get; set; }

  private AccountRetrieveResponse(Guid id, string name, string email, string phone) {
    this.Id = id;
    this.Name = name;
    this.Email = email;
    this.Phone = phone;
  }

  public static AccountRetrieveResponse From(AccountEntity entity) {
    return new AccountRetrieveResponse(entity.Id, entity.Name, entity.Email, entity.Phone);
  }

  public static AccountRetrieveResponse[] From(AccountEntity[] entities) {
    return entities.Select(x => new AccountRetrieveResponse(x.Id, x.Name, x.Email, x.Phone)).ToArray();
  }
}
