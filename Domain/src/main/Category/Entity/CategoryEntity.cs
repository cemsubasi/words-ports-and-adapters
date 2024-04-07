using Domain.Post.Entity;

namespace Domain.Category.Entity;

public class CategoryEntity {
  public Guid Id { get; set; }
  public virtual List<PostEntity> Posts { get; set; } = new();
  public string Category { get; set; }
}
