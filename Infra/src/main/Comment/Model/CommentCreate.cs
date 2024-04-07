namespace Infra.Comment.Model;

public class CommentCreate {
  private Guid accountId;
  private Guid postId;
  private Guid? parentCommentId;
  private string comment;

  public CommentCreate(Guid accountId, Guid postId, Guid? parentCommentId, string comment) {
    this.accountId = accountId;
    this.postId = postId;
    this.parentCommentId = parentCommentId;
    this.comment = comment;
  }
}
