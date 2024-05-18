using Domain.Question;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Question.Controller;

[Authorize]
[ApiController]
[Route("[controller]")]
public class QuestionController : ControllerBase {
  private readonly IUserSession session;
  private readonly QuestionCreateUseCaseHandler createUseCaseHandler;
  private readonly QuestionRetrieveUseCaseHandler retrieveUseCaseHandler;
  private readonly QuestionDeleteUseCaseHandler deleteUseCaseHandler;

  public QuestionController(QuestionCreateUseCaseHandler createUseCaseHandler, IUserSession session, QuestionRetrieveUseCaseHandler retrieveUseCaseHandler, QuestionDeleteUseCaseHandler deleteUseCaseHandler) {
    this.session = session;
    this.createUseCaseHandler = createUseCaseHandler;
    this.retrieveUseCaseHandler = retrieveUseCaseHandler;
    this.deleteUseCaseHandler = deleteUseCaseHandler;
  }

  [HttpPost]
  public async Task<IActionResult> CreateQuestion([FromBody] QuestionCreateRequest request, CancellationToken cancellationToken) {
    await this.createUseCaseHandler.Handle(request.ToUseCase(this.session.Id), cancellationToken);

    return this.NoContent();
  }

  [HttpPost("by-category")]
  public async Task<IActionResult> RetrieveQuestionsByCategoryId([FromBody] QuestionRetrieveRequest request, CancellationToken cancellationToken) {
    var result = await this.retrieveUseCaseHandler.Handle(request.ToUseCase(this.session.Id), cancellationToken);

    return this.Ok(QuestionRetrieveResponse.From(result));
  }

  [HttpDelete]
  public async Task<IActionResult> DeleteQuestion([FromBody] QuestionDeleteRequest request, CancellationToken cancellationToken) {
    await this.deleteUseCaseHandler.Handle(request.ToUseCase(this.session.Id), cancellationToken);

    return this.NoContent();
  }
}
