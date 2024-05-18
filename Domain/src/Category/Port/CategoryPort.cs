using Domain.Category.Entity;
using Domain.Category.UseCase;
using Domain.Common;

namespace Domain.Category.Port;

public interface CategoryPort {
  Task CreateAsync(CategoryCreate useCase, CancellationToken cancellationToken);
  Task<CategoryEntity> Retrieve(CategoryRetrieve useCase, CancellationToken cancellationToken);
  Task<CategoryEntity[]> Retrieve(CategoryRetrieveAll useCase, CancellationToken cancellationToken);
  Task DeleteAsync(CategoryDelete useCase, CancellationToken cancellationToken);
}
