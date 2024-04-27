using Domain.Account.Entity;
using Domain.Account.Port;
using Domain.Account.UseCase;

namespace Domain.Account;

public class AccountCreateUseCaseHandler(AccountPort accountPort) {
  private readonly AccountPort accountPort = accountPort;

  public async Task<AccountEntity> Handle(AccountCreate accountCreate, CancellationToken cancellationToken) {
    return await this.accountPort.CreateAsync(accountCreate, cancellationToken);
  }
}
