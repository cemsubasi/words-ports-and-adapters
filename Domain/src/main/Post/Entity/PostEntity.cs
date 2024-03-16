using Domain.Account.Entity;
using Domain.Category.Entity;
using Domain.Comment.Entity;

namespace Domain.Post.Entity;

public class PostEntity {
  public Guid Id { get; set; }
  public Guid AccountId { get; set; }
  public virtual AccountEntity Account { get; set; }
  public Guid CategoryId { get; set; }
  public virtual CategoryEntity Category { get; set; }
  public virtual ICollection<CommentEntity> Comments { get; set; }
  public string Thumbnail { get; set; }
  public string Url { get; set; }
  public string Header { get; set; }
  public string Body { get; set; }
  public bool IsFeatured { get; set; }
  public DateTimeOffset CreatedAt { get; set; }
  public DateTimeOffset UpdatedAt { get; set; }
}
