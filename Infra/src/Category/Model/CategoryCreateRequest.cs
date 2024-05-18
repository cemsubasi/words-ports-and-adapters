using Domain.Category.UseCase;

namespace Infra.Category;

public class CategoryCreateRequest {
  public string Name { get; set; }

  public CategoryCreate ToUseCase(Guid id) {
    return CategoryCreate.Build(id, this.Name);
  }
}
