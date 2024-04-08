using Domain.Category.Entity;
using Domain.Category.Port;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Category.Adapter;

public class CategoryAdapter(MainDbContext context) : CategoryPort {
  private readonly MainDbContext context = context;

  public async Task<CategoryEntity> FindOrCreateCategoryAsync(string categoryName, CancellationToken cancellationToken) {
    var category = await this.context.Categories
        .AsNoTracking()
        .SingleOrDefaultAsync(x => x.Category == categoryName, cancellationToken);

    if (category is null) {
      category = CategoryEntity.Create(
        id: Guid.NewGuid(),
        category: categoryName);

      await this.context.Categories.AddAsync(category, cancellationToken);
      await this.context.SaveChangesAsync(cancellationToken);
    }

    return category;
  }

}
