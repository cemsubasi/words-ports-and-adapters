using Domain.Common;

namespace Domain.Comment.UseCase;

public class RetrieveAllComments : DataRequest {
  public Guid ParentCommentId { get; private set; }

  private RetrieveAllComments(Guid parentCommentId, int page, int size) : base(page, size) {
    this.ParentCommentId = parentCommentId;
  }

  public static RetrieveAllComments Build(Guid parentCommentId, int page, int size) {
    return new RetrieveAllComments(parentCommentId, page, size);
  }
}
