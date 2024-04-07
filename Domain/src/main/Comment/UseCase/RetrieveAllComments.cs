using Domain.Common;

namespace Domain.Comment.UseCase;

public class RetrieveAllComments : DataRequest {
  public Guid ParentCommentId { get; private set; }

  public RetrieveAllComments(Guid parentCommentId, int page, int size) : base(page, size) {
    this.ParentCommentId = parentCommentId;
  }

  public RetrieveAllComments Build(Guid parentCommentId) {
    return new RetrieveAllComments(parentCommentId, this.Page, this.Size);
  }
}
