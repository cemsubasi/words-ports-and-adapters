using Domain.Category;
using Domain.Category.UseCase;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Category.Controller;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase {
  private readonly IUserSession session;
  private readonly CategoryCreateUseCaseHandler categoryCreateUseCaseHandler;
  private readonly CategoryRetrieveUseCaseHandler categoryRetrieveUseCaseHandler;
  private readonly CategoryDeleteUseCaseHandler categoryDeleteUseCaseHandler;

  public CategoryController(
      IUserSession session,
      CategoryRetrieveUseCaseHandler categoryRetrieveUseCaseHandler,
      CategoryCreateUseCaseHandler categoryCreateUseCaseHandler,
      CategoryDeleteUseCaseHandler categoryDeleteUseCaseHandler) {
    this.session = session;
    this.categoryRetrieveUseCaseHandler = categoryRetrieveUseCaseHandler;
    this.categoryCreateUseCaseHandler = categoryCreateUseCaseHandler;
    this.categoryDeleteUseCaseHandler = categoryDeleteUseCaseHandler;
  }

  [HttpPost]
  public async Task<IActionResult> CreateAsync(CategoryCreateRequest request, CancellationToken cancellationToken) {
    await this.categoryCreateUseCaseHandler.Handle(request.ToUseCase(this.session.Id), cancellationToken);

    return this.NoContent();
  }

  [HttpGet]
  public async Task<IActionResult> RetrieveAsync([FromQuery] Guid id, CancellationToken cancellationToken) {
    var result = await this.categoryRetrieveUseCaseHandler.Handle(CategoryRetrieve.Build(id: id, accountId: this.session.Id), cancellationToken);

    return this.Ok(CategoryRetrieveResponse.From(result));
  }

  [HttpPost("all")]
  public async Task<IActionResult> RetrieveAsync([FromBody] DataRequest request, CancellationToken cancellationToken) {
    var categoryRetrieveAll = new CategoryRetrieveAllRequest(this.session.Id, request.Page, request.Size);
    var result = await this.categoryRetrieveUseCaseHandler.Handle(categoryRetrieveAll.ToUseCase(), cancellationToken);

    return this.Ok(CategoryRetrieveResponse.From(result));
  }

  [HttpDelete]
  public async Task<IActionResult> DeleteAsync([FromBody] CategoryDeleteRequest request, CancellationToken cancellationToken) {
    await this.categoryDeleteUseCaseHandler.Handle(request.ToUseCase(this.session.Id), cancellationToken);

    return this.NoContent();
  }
}
