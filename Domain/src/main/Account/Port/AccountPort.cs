using Domain.Account.Entity;
using Domain.Account.UseCase;
using Domain.Common;

namespace Domain.Account.Port;

public interface AccountPort {
  Task<AccountEntity> Retrieve(AccountRetrieve accountRetrieve, CancellationToken cancellationToken);
  Task<AccountEntity[]> Retrieve(DataRequest dataRequest, CancellationToken cancellationToken);
  Task<AccountEntity> CreateAsync(AccountEntity accountEntity, CancellationToken cancellationToken);
  bool Delete(Guid id);
  bool Update(Guid id);
  Task<(string, long)> Authenticate(AccountAuthenticate accountAuthenticate, CancellationToken cancellationToken);
}
