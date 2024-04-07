using Domain.Account.Entity;
using Domain.Post.Entity;

namespace Domain.Comment.Entity;

public class CommentEntity {
  public Guid Id { get; private set; }
  public Guid PostId { get; private set; }
  public PostEntity Post { get; private set; }
  public Guid? ParentCommentId { get; private set; }
  public CommentEntity ParentComment { get; private set; }
  public List<CommentEntity> SubComments { get; private set; } = new();
  public string Comment { get; private set; }
  public DateTimeOffset CreatedAt { get; private set; }
  public Guid? CreatedBy { get; private set; }
  public virtual AccountEntity Creator { get; private set; }

  protected CommentEntity(Guid id, Guid postId, Guid? parentCommentId, string comment, Guid? createdBy = null) {
    Id = id;
    PostId = postId;
    ParentCommentId = parentCommentId;
    Comment = comment;
    CreatedAt = DateTimeOffset.UtcNow;
    CreatedBy = createdBy;
  }

  public static CommentEntity Create(Guid id, Guid postId, Guid? parentCommentId, string comment, Guid? createdBy = null) {
    return new CommentEntity(id, postId, parentCommentId, comment, createdBy);
  }
}
