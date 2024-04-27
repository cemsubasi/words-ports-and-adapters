
using Domain.Identity.Entity;
using Domain.Identity.Port;
using Domain.Identity.UseCase;
using Infra.Context;

namespace Infra.Identity.Adapter;

public class IdentityAdapter(MainDbContext context, IUserSession userSession) : IdentityPort {
    private readonly MainDbContext context = context;
    private readonly IUserSession userSession = userSession;
    public async Task<IdentityEntity> CreateAsync(IdentityCreate useCase, CancellationToken cancellationToken) {
        var entity = IdentityEntity.Create(
            inetAddress: useCase.InetAddress,
            userAgent: useCase.UserAgent,
            language: useCase.Language,
            accountId: this.userSession.Id
        );

        await this.context.AddAsync(entity, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);

        return entity;
    }
}