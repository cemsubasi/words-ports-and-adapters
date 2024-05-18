﻿using Domain.Identity.Entity;
using Domain.Identity.UseCase;

namespace Domain.Identity.Port;

public interface IdentityPort {
  Task<IdentityEntity> CreateAsync(IdentityCreate accountCreate, CancellationToken cancellationToken);
  // Task<IdentityEntity> Retrieve(IdentityRetrieve visitorRetrieve, CancellationToken cancellationToken);
  // Task<IdentityEntity[]> Retrieve(DataRequest dataRequest, CancellationToken cancellationToken);
  // bool Delete(Guid id);
  // bool Update(Guid id);
}
