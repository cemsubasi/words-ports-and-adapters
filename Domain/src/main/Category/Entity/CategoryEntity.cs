using Domain.Post.Entity;

namespace Domain.Category.Entity;

public class CategoryEntity {
  public Guid Id { get; set; }
  public virtual ICollection<PostEntity> Posts { get; set; }
  public string Category { get; set; }
}
