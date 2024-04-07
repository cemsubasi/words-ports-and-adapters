using Domain.Post.Entity;

namespace Domain.Category.Entity;

public class CategoryEntity {
  public Guid Id { get; set; }
  public virtual List<PostEntity> Posts { get; set; } = new();
  public string Category { get; set; }

  protected CategoryEntity(Guid id, string category) {
    Id = id;
    Category = category;
  }

  public static CategoryEntity Create(Guid id, string category) {
    return new CategoryEntity(id, category);
  }
}
