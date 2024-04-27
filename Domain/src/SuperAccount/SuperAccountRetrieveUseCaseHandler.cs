using Domain.Common;
using Domain.SuperAccount.Entity;
using Domain.SuperAccount.Port;
using Domain.SuperAccount.UseCase;

namespace Domain.SuperAccount;

public class SuperAccountRetrieveUseCaseHandler(SuperAccountPort accountPort) {
  private readonly SuperAccountPort accountPort = accountPort;

  public async Task<SuperAccountEntity> Handle(SuperAccountRetrieve accountRetrieve, CancellationToken cancellationToken) {
    return await this.accountPort.Retrieve(accountRetrieve, cancellationToken);
  }

  public async Task<SuperAccountEntity[]> Handle(DataRequest accountRetrieve, CancellationToken cancellationToken) {
    return await this.accountPort.Retrieve(accountRetrieve, cancellationToken);
  }
}
