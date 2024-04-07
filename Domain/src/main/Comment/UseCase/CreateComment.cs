namespace Domain.Comment.UseCase;

public class CreateComment {
  public Guid? AccountId { get; }
  public Guid PostId { get; }
  public string Comment { get; }
  public Guid? ParentCommentId { get; }

  private CreateComment(Guid? accountId, Guid postId, Guid? parentCommentId, string comment) {
    this.AccountId = accountId;
    this.PostId = postId;
    this.ParentCommentId = parentCommentId;
    this.Comment = comment;
  }

  public static CreateComment Build(Guid? accountId, Guid postId, Guid? parentCommentId, string comment) {
    return new CreateComment(accountId, postId, parentCommentId, comment);
  }
}
