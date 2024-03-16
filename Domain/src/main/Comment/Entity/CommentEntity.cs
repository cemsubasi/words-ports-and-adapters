using Domain.Post.Entity;

namespace Domain.Comment.Entity;

public class CommentEntity {
  public Guid Id { get; set; }
  public Guid PostId { get; set; }
  public PostEntity Post { get; set; }
  public Guid? ParentCommentId { get; set; }
  public CommentEntity ParentComment { get; set; }
  public string Comment { get; set; }
  public DateTimeOffset CreatedAt { get; set; }
  public string CreatedBy { get; set; }
}