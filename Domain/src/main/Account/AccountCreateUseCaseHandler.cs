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
    return await accountPort.CreateAsync(accountCreate, cancellationToken);
  }
}
