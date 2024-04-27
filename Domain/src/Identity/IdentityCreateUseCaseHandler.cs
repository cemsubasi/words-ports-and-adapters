using Domain.Identity.Entity;
using Domain.Identity.Port;
using Domain.Identity.UseCase;

namespace Domain.Identity;

public class VisitorCreateUseCaseHandler {
  private readonly IdentityPort Identity;

  public VisitorCreateUseCaseHandler(IdentityPort identity) {
    this.Identity = identity;
  }

  public async Task<IdentityEntity> Handle(IdentityCreate identityCreate, CancellationToken cancellationToken) {
    return await Identity.CreateAsync(identityCreate, cancellationToken);
  }
}
