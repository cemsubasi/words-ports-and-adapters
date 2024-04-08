using Domain.Comment.UseCase;
using Domain.Common;

namespace Infra.Comment.Model;

public class RetrieveAllCommentsRequest : DataRequest {
  public Guid ParentCommentId { get; private set; }

  private RetrieveAllCommentsRequest(Guid parentCommentId, int page, int size) : base(page, size) {
    this.ParentCommentId = parentCommentId;
  }

  public RetrieveAllComments ToUseCase() {
    return RetrieveAllComments.Build(this.ParentCommentId, this.Page, this.Size);
  }
}
