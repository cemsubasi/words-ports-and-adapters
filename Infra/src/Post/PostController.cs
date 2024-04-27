using Domain.Comment;
using Domain.Post;
using Domain.Post.UseCase;
using Infra.Comment.Model;
using Infra.Post.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Post.Controller;

[Authorize]
[ApiController]
[Route("[controller]")]
public class PostController(IUserSession session, CreatePostUseCaseHandler createUseCaseHandler, RetrievePostUseCaseHandler retrieveUseCaseHandler, AddCommentUseCaseHandler addCommentUseCaseHandler, RetrieveCommentUseCaseHandler retrieveCommentUseCaseHandler) : ControllerBase {
  private readonly IUserSession session = session;
  private readonly CreatePostUseCaseHandler createUseCaseHandler = createUseCaseHandler;
  private readonly RetrievePostUseCaseHandler retrieveUseCaseHandler = retrieveUseCaseHandler;
  private readonly AddCommentUseCaseHandler addCommentUseCaseHandler = addCommentUseCaseHandler;
  private readonly RetrieveCommentUseCaseHandler retrieveCommentUseCaseHandler = retrieveCommentUseCaseHandler;

  [HttpPost]
  public async Task<IActionResult> Create(PostCreateRequest request, CancellationToken cancellationToken) {
    await this.createUseCaseHandler.Handle(request.ToUseCase(this.session.Id), cancellationToken);

    return this.NoContent();
  }

  [HttpGet]
  public async Task<IActionResult> Retrieve([FromQuery] Guid id, CancellationToken cancellationToken) {
    var result = await this.retrieveUseCaseHandler.Handle(PostRetrieveRequest.ToUseCase(id, this.session.Id), cancellationToken);

    return this.Ok(PostRetrieveResponse.From(result));
  }

  [HttpPost("all")]
  public async Task<IActionResult> Retrieve([FromBody] PostRetrieveAllRequest request, CancellationToken cancellationToken) {
    var result = await this.retrieveUseCaseHandler.Handle(request.ToUseCase(this.session.Id), cancellationToken);

    return this.Ok(PostRetrieveResponse.From(result));
  }

  [AllowAnonymous]
  [HttpPost("comment")]
  public async Task<IActionResult> AddComment(CommentCreateRequest request, CancellationToken cancellationToken) {
    await this.addCommentUseCaseHandler.Handle(request.ToUseCase(), cancellationToken);

    return this.NoContent();
  }


  [AllowAnonymous]
  [HttpPost("comment/all")]
  public async Task<IActionResult> RetrieveComments([FromBody] CommentRetrieveAllRequest request, CancellationToken cancellationToken) {
    var result = await this.retrieveCommentUseCaseHandler.Handle(request.ToUseCase(), cancellationToken);

    return this.Ok(CommentRetrieveResponse.From(result));
  }
}
