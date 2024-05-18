using Domain.Category.Entity;
using Domain.Category.Port;
using Domain.Category.UseCase;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Category.Adapter;

public class CategoryAdapter : CategoryPort {
  private readonly MainDbContext context;
  public CategoryAdapter(MainDbContext context) {
    this.context = context;
  }

  public async Task CreateAsync(CategoryCreate useCase, CancellationToken cancellationToken) {
    var isExist = await this.context.Categories.AnyAsync(x => x.AccountId == useCase.AccountId && x.Name == useCase.Name);
    if (isExist) {
      throw new Exception("Category already exist.");
    }

    var entity = CategoryEntity.Create(useCase.Name, useCase.AccountId);
    await this.context.Categories.AddAsync(entity, cancellationToken);
    var result = await this.context.SaveChangesAsync(cancellationToken);
    if (result.Equals(0)) {
      throw new Exception("An error occoured while saving changes.");
    }
  }

  public async Task DeleteAsync(CategoryDelete useCase, CancellationToken cancellationToken) {
    var category = await this.context.Categories
      .SingleOrDefaultAsync(x => x.Id == useCase.Id && x.AccountId == useCase.AccountId, cancellationToken);

    if (category is null) {
      throw new Exception($"Category not found with id: {useCase.Id}");
    }

    _ = this.context.Categories.Remove(category);
    var result = await this.context.SaveChangesAsync(cancellationToken);
    if (result.Equals(0)) {
      throw new Exception("An error occoured while saving changes.");
    }
  }

  public async Task<CategoryEntity> Retrieve(CategoryRetrieve useCase, CancellationToken cancellationToken) {
    var category = await this.context.Categories
      .AsNoTrackingWithIdentityResolution()
      .Include(x => x.Questions)
      .SingleOrDefaultAsync(x => x.AccountId == useCase.AccountId && x.Id == useCase.Id);

    if (category is null) {
      throw new Exception($"Category not found with id: {useCase.Id}");
    }

    return category;
  }

  public async Task<CategoryEntity[]> Retrieve(CategoryRetrieveAll useCase, CancellationToken cancellationToken) {
    var categories = await this.context.Categories
      .AsNoTrackingWithIdentityResolution()
      .Where(x => x.AccountId == useCase.AccountId)
      .Include(x => x.Questions)
      .Skip(useCase.Page * useCase.Size)
      .Take(useCase.Size)
      .ToArrayAsync(cancellationToken);

    return categories;
  }
}
