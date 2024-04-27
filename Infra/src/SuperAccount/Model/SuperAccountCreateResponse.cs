using Domain.SuperAccount.Entity;

namespace Infra.Controllers.SuperAccount;

public class SuperAccountCreateResponse {
  public Guid Id { get; private set; }
  public string Name { get; private set; }
  public string Email { get; private set; }

  private SuperAccountCreateResponse(Guid id, string name, string email) {
    this.Id = id;
    this.Name = name;
    this.Email = email;
  }

  public static SuperAccountCreateResponse From(SuperAccountEntity accountEntity) {
    return new SuperAccountCreateResponse(accountEntity.Id, accountEntity.Name, accountEntity.Email);
  }
}
