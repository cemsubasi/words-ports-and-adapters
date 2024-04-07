using Domain.SuperAccount.Entity;
using Domain.SuperAccount.Port;
using Domain.SuperAccount.UseCase;

namespace Domain.SuperAccount;

public class SuperAccountCreateUseCaseHandler {
  private readonly SuperAccountPort accountPort;

  public SuperAccountCreateUseCaseHandler(SuperAccountPort accountPort) {
    this.accountPort = accountPort;
  }

  public async Task<SuperAccountEntity> Handle(SuperAccountCreate accountCreate, CancellationToken cancellationToken) {
    return await accountPort.CreateAsync(accountCreate, cancellationToken);
  }
}
