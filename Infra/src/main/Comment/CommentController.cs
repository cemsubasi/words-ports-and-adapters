using Domain.Comment;
using Infra.Comment.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Comment.Controller;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase {
  readonly CreateCommentUseCaseHandler createUseCaseHandler;
  readonly RetrieveCommentUseCaseHandler retrieveUseCaseHandler;

  public CommentController(CreateCommentUseCaseHandler createUseCaseHandler, RetrieveCommentUseCaseHandler retrieveUseCaseHandler) {
    this.createUseCaseHandler = createUseCaseHandler;
    this.retrieveUseCaseHandler = retrieveUseCaseHandler;
  }

  [HttpPost]
  [AllowAnonymous]
  public async Task<IActionResult> Create(CommentCreateRequest request, CancellationToken cancellationToken) {
    await this.createUseCaseHandler.Handle(request.ToUseCase(), cancellationToken);

    return this.NoContent();
  }

  [HttpPost("all")]
  public async Task<IActionResult> RetrieveAll([FromBody] RetrieveAllCommentsRequest request, CancellationToken cancellationToken) {
    var result = await this.retrieveUseCaseHandler.HandleAsync(request.ToUseCase(), cancellationToken);

    return this.Ok(result.Select(CommentRetrieveResponse.From).ToArray());
  }
}
