using Domain.Comment.UseCase;

namespace Infra.Comment.Model;

public class CommentCreateRequest {
  public Guid PostId { get; set; }
  public Guid? ParentCommentId { get; set; }
  public string Comment { get; set; }
  public Guid AccountId { get; set; }

  public CreateComment ToUseCase() {
    return CreateComment.Build(this.AccountId, this.PostId, this.ParentCommentId, this.Comment);
  }
}
