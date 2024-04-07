using Domain.Account.Entity;
using Domain.Account.Port;
using Domain.Account.UseCase;
using Domain.Common;

namespace Domain.Account;

public class AccountRetrieveUseCaseHandler {
  private readonly AccountPort accountPort;

  public AccountRetrieveUseCaseHandler(AccountPort accountPort) {
    this.accountPort = accountPort;
  }

  public async Task<AccountEntity> Handle(AccountRetrieve accountRetrieve, CancellationToken cancellationToken) {
    return await accountPort.Retrieve(accountRetrieve, cancellationToken);
  }

  public async Task<AccountEntity[]> Handle(DataRequest accountRetrieve, CancellationToken cancellationToken) {
    return await accountPort.Retrieve(accountRetrieve, cancellationToken);
  }
}
