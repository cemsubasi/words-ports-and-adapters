using Domain.SuperAccount.Entity;

namespace Infra.Controllers.SuperAccount;

public class SuperAccountRetrieveResponse {
  public Guid Id { get; private set; }

  public string Name { get; private set; }

  public string Email { get; private set; }

  public string Phone { get; set; }

  private SuperAccountRetrieveResponse(Guid id, string name, string email, string phone) {
    this.Id = id;
    this.Name = name;
    this.Email = email;
    this.Phone = phone;
  }

  public static SuperAccountRetrieveResponse From(SuperAccountEntity entity) {
    return new SuperAccountRetrieveResponse(entity.Id, entity.Name, entity.Email, entity.Phone);
  }

  public static SuperAccountRetrieveResponse[] From(SuperAccountEntity[] entities) {
    return entities.Select(x => new SuperAccountRetrieveResponse(x.Id, x.Name, x.Email, x.Phone)).ToArray();
  }
}
