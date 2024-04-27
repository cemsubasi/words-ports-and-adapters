using Domain.Account.Port;
using Domain.Account.UseCase;

namespace Domain.Account;

public class AccountAuthenticateUseCaseHandler(AccountPort accountPort) {
  private readonly AccountPort accountPort = accountPort;

  public async Task<(string, long)> Handle(AccountAuthenticate accountAuthenticate, CancellationToken cancellationToken) {
    return await this.accountPort.Authenticate(accountAuthenticate, cancellationToken);
  }
}
