using Domain.Identity.Port;

namespace Domain.Identity;

public class IdentityRetrieveUseCaseHandler {
  private readonly IdentityPort identityPort;

  public IdentityRetrieveUseCaseHandler(IdentityPort identityPort) {
    this.identityPort = identityPort;
  }

  // public async Task<IdentityEntity> Handle(IdentityRetrieve visitorRetrieve, CancellationToken cancellationToken) {
  //   return await identityPort.Retrieve(visitorRetrieve, cancellationToken);
  // }

  // public async Task<IdentityEntity[]> Handle(DataRequest visitorRetrieve, CancellationToken cancellationToken) {
  //   return await identityPort.Retrieve(visitorRetrieve, cancellationToken);
  // }
}
