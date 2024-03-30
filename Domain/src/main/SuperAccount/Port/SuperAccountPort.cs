using Domain.Common;
using Domain.SuperAccount.Entity;
using Domain.SuperAccount.UseCase;

namespace Domain.SuperAccount.Port;

public interface SuperAccountPort {
  Task<SuperAccountEntity> Retrieve(SuperAccountRetrieve accountRetrieve, CancellationToken cancellationToken);
  Task<SuperAccountEntity[]> Retrieve(DataRequest dataRequest, CancellationToken cancellationToken);
  Task<SuperAccountEntity> CreateAsync(SuperAccountCreate accountCreate, CancellationToken cancellationToken);
  bool Delete(Guid id);
  bool Update(Guid id);
  Task<(string, long)> Authenticate(SuperAccountAuthenticate accountAuthenticate, CancellationToken cancellationToken);
}
