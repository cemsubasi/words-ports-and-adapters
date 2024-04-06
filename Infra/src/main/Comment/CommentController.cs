using Domain.Comment;
using Infra.Comment.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Comment.Controller;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase {
  readonly CreateCommentUseCaseHandler createUseCaseHandler;

  public CommentController(CreateCommentUseCaseHandler createUseCaseHandler) {
    this.createUseCaseHandler = createUseCaseHandler;
  }

  [HttpPost]
  [AllowAnonymous]
  public async Task<IActionResult> Create(CommentCreateRequest request, CancellationToken cancellationToken) {
    await this.createUseCaseHandler.Handle(request.ToUseCase(), cancellationToken);

    return this.NoContent();
  }
}
