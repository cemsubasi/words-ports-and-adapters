using Domain.SuperAccount.Entity;
using Domain.SuperAccount.Port;
using Domain.SuperAccount.UseCase;

namespace Domain.SuperAccount;

public class SuperAccountCreateUseCaseHandler(SuperAccountPort accountPort) {
  private readonly SuperAccountPort accountPort = accountPort;

  public async Task<SuperAccountEntity> Handle(SuperAccountCreate accountCreate, CancellationToken cancellationToken) {
    return await this.accountPort.CreateAsync(accountCreate, cancellationToken);
  }
}
