using Domain.Comment.UseCase;
using Domain.Common;

namespace Infra.Comment.Model;

public class CommentRetrieveAllRequest(Guid parentCommentId, int page, int size) : DataRequest(page, size) {
  public Guid ParentCommentId { get; private set; } = parentCommentId;

  public RetrieveAllComments ToUseCase() {
    return RetrieveAllComments.Build(this.ParentCommentId, this.Page, this.Size);
  }
}
