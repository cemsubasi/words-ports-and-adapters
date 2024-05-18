using Domain.Category.Port;
using Domain.Category.UseCase;

namespace Domain.Category;

public class CategoryDeleteUseCaseHandler {
  private readonly CategoryPort categoryPort;
  public CategoryDeleteUseCaseHandler(CategoryPort categoryPort) {
    this.categoryPort = categoryPort;
  }
  public async Task Handle(CategoryDelete useCase, CancellationToken cancellationToken) {
    await this.categoryPort.DeleteAsync(useCase, cancellationToken);
  }
}
