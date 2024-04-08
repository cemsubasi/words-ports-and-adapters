using Domain.Category.Entity;

namespace Domain.Category.Port;

public interface CategoryPort {
  Task<CategoryEntity> FindOrCreateCategoryAsync(string categoryName, CancellationToken cancellationToken);
}
