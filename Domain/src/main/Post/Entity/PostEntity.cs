using Domain.Account.Entity;
using Domain.Category.Entity;
using Domain.Comment.Entity;

namespace Domain.Post.Entity;

public sealed class PostEntity {
  public Guid Id { get; private set; }
  public Guid AccountId { get; private set; }
  public AccountEntity Account { get; private set; }
  public Guid CategoryId { get; private set; }
  public CategoryEntity Category { get; private set; }
  public List<CommentEntity> Comments { get; private set; } = [];
  public string Thumbnail { get; private set; }
  public string Url { get; private set; }
  public string Header { get; private set; }
  public string Body { get; private set; }
  public bool IsFeatured { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public DateTime? UpdatedAt { get; private set; }
  public DateTime? DeletedAt { get; private set; }

  private PostEntity(Guid id, Guid accountId, Guid categoryId, string thumbnail, string url, string header, string body, bool isFeatured) {
    this.Id = id;
    this.AccountId = accountId;
    this.CategoryId = categoryId;
    this.Thumbnail = thumbnail;
    this.Url = url;
    this.Header = header;
    this.Body = body;
    this.IsFeatured = isFeatured;
    this.CreatedAt = DateTimeOffset.UtcNow;
    this.UpdatedAt = DateTimeOffset.UtcNow;
  }

  public static PostEntity Create(
      Guid id,
      Guid accountId,
      Guid categoryId,
      string thumbnail,
      string url,
      string header,
      string body,
      bool isFeatured) {
    return new PostEntity(
        id,
        accountId,
        categoryId,
        thumbnail,
        url,
        header,
        body,
        isFeatured);
  }
}
