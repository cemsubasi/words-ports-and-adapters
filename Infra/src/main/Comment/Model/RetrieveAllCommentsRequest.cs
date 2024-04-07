using Domain.Comment.UseCase;
using Domain.Common;

namespace Infra.Comment.Model;

public class RetrieveAllCommentsRequest : DataRequest {
  public Guid ParentCommentId { get; private set; }

  public RetrieveAllCommentsRequest(Guid parentCommentId, int page, int size) : base(page, size) {
    this.ParentCommentId = parentCommentId;
  }

  public RetrieveAllComments ToUseCase() {
    return new RetrieveAllComments(this.ParentCommentId, this.Page, this.Size);
  }
}
