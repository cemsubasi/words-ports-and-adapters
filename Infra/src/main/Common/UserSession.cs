using Domain.Account.Entity;

namespace Infra;

public class UserSession : IUserSession {
  public Guid Id { get; set; }
  /* public IEnumerable<AccountEntity> User { get; set; } */
}
