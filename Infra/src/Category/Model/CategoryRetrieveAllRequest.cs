using Domain.Category.UseCase;
using Domain.Common;

namespace Infra.Category;

public class CategoryRetrieveAllRequest(Guid accountId, int page, int size) : DataRequest(page, size) {
  public Guid AccountId { get; set; } = accountId;

  public CategoryRetrieveAll ToUseCase() {
    return new CategoryRetrieveAll {
      AccountId = this.AccountId,
      Page = this.Page,
      Size = this.Size,
    };
  }
}
