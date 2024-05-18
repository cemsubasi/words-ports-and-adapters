namespace Infra;

public interface IUserSession {
  Guid Id { get; set; }
  /* Func<AccountEntity, bool> User { get; set; } */
}
