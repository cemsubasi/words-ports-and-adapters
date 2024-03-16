using Domain.Common;
using Domain.Post;
using Infra.Post.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Post.Controller;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PostController : ControllerBase {
  private readonly IUserSession session;
  private readonly CreatePostUseCaseHandler createUseCaseHandler;
  private readonly RetrievePostUseCaseHandler retrieveUseCaseHandler;

  public PostController(IUserSession session, CreatePostUseCaseHandler createUseCaseHandler, RetrievePostUseCaseHandler retrieveUseCaseHandler) {
    this.session = session;
    this.createUseCaseHandler = createUseCaseHandler;
    this.retrieveUseCaseHandler = retrieveUseCaseHandler;
  }

  [HttpPost]
  public async Task<IActionResult> CreatePost(PostCreateRequest request, CancellationToken cancellationToken) {
    await this.createUseCaseHandler.Handle(request.ToUseCase(this.session.Id), cancellationToken);

    return this.NoContent();
  }

  [HttpGet]
  public async Task<IActionResult> RetrievePost([FromQuery] Guid id, CancellationToken cancellationToken) {
    var result = await this.retrieveUseCaseHandler.Handle(PostRetrieveRequest.ToUseCase(id, this.session.Id), cancellationToken);

    return this.Ok(PostRetrieveResponse.From(result));
  }

  [HttpPost("all")]
  public async Task<IActionResult> CreatePost(DataRequest request, CancellationToken cancellationToken) {
    var result = await this.retrieveUseCaseHandler.Handle(request.Build(this.session.Id), cancellationToken);

    return this.Ok(PostRetrieveResponse.From(result));
  }
}
