using Domain.SuperAccount.Port;
using Domain.SuperAccount.UseCase;

namespace Domain.SuperAccount;

public class SuperAccountAuthenticateUseCaseHandler {
  private readonly SuperAccountPort accountPort;

  public SuperAccountAuthenticateUseCaseHandler(SuperAccountPort accountPort) {
    this.accountPort = accountPort;
  }

  public async Task<(string, long)> Handle(SuperAccountAuthenticate accountAuthenticate, CancellationToken cancellationToken) {
    return await this.accountPort.Authenticate(accountAuthenticate, cancellationToken);
  }
}
