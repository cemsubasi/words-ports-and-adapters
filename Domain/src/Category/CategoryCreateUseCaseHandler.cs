using Domain.Category.Port;
using Domain.Category.UseCase;

namespace Domain.Category;

public class CategoryCreateUseCaseHandler {
  private readonly CategoryPort categoryPort;

  public CategoryCreateUseCaseHandler(CategoryPort categoryPort) => this.categoryPort = categoryPort;

  public async Task Handle(CategoryCreate useCase, CancellationToken cancellationToken) {
    await this.categoryPort.CreateAsync(useCase, cancellationToken);
  }
}
