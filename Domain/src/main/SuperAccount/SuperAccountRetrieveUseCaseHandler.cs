using Domain.Common;
using Domain.SuperAccount.Entity;
using Domain.SuperAccount.Port;
using Domain.SuperAccount.UseCase;

namespace Domain.SuperAccount;

public class SuperAccountRetrieveUseCaseHandler {
  private readonly SuperAccountPort accountPort;

  public SuperAccountRetrieveUseCaseHandler(SuperAccountPort accountPort) {
    this.accountPort = accountPort;
  }

  public async Task<SuperAccountEntity> Handle(SuperAccountRetrieve accountRetrieve, CancellationToken cancellationToken) {
    return await accountPort.Retrieve(accountRetrieve, cancellationToken);
  }

  public async Task<SuperAccountEntity[]> Handle(DataRequest accountRetrieve, CancellationToken cancellationToken) {
    return await accountPort.Retrieve(accountRetrieve, cancellationToken);
  }
}
