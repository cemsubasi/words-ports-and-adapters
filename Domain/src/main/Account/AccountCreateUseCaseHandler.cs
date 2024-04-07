using Domain.Account.Entity;
using Domain.Account.Port;
using Domain.Account.UseCase;

namespace Domain.Account;

public class AccountCreateUseCaseHandler {
  private readonly AccountPort accountPort;

  public AccountCreateUseCaseHandler(AccountPort accountPort) {
    this.accountPort = accountPort;
  }

  public async Task<AccountEntity> Handle(AccountCreate accountCreate, CancellationToken cancellationToken) {
    var accountEntity = AccountEntity.Create(
        id: Guid.NewGuid(),
        email: accountCreate.Email,
        name: accountCreate.Name,
        phone: accountCreate.Phone,
        password: accountCreate.Password,
        passwordSalt: accountCreate.PasswordSalt
        );

    return await accountPort.CreateAsync(accountEntity, cancellationToken);
  }
}
