using Domain.Category.Entity;
using Domain.Category.Port;
using Domain.Category.UseCase;
using Domain.Common;

namespace Domain.Category;

public class CategoryRetrieveUseCaseHandler {
  private readonly CategoryPort categoryPort;

  public CategoryRetrieveUseCaseHandler(CategoryPort categoryPort) => this.categoryPort = categoryPort;

  public async Task<CategoryEntity> Handle(CategoryRetrieve useCase, CancellationToken cancellationToken) {
    return await this.categoryPort.Retrieve(useCase, cancellationToken);
  }

  public async Task<CategoryEntity[]> Handle(CategoryRetrieveAll useCase, CancellationToken cancellationToken) {
    return await this.categoryPort.Retrieve(useCase, cancellationToken);
  }
}
