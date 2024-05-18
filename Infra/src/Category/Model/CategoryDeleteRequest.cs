using Domain.Category.UseCase;

namespace Infra.Category;

public class CategoryDeleteRequest {
  public Guid Id { get; set; }

  public CategoryDelete ToUseCase(Guid accountId) {
    return CategoryDelete.Build(id: this.Id, accountId: accountId);
  }
}
