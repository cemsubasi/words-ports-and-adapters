using Domain.Account.Entity;
using Domain.Account.Port;
using Domain.Account.UseCase;
using Domain.Common;

namespace Domain.Account;

public class AccountRetrieveUseCaseHandler(AccountPort accountPort) {
  private readonly AccountPort accountPort = accountPort;

  public async Task<AccountEntity> Handle(AccountRetrieve accountRetrieve, CancellationToken cancellationToken) {
    return await this.accountPort.Retrieve(accountRetrieve, cancellationToken);
  }

  public async Task<AccountEntity[]> Handle(DataRequest accountRetrieve, CancellationToken cancellationToken) {
    return await this.accountPort.Retrieve(accountRetrieve, cancellationToken);
  }
}
