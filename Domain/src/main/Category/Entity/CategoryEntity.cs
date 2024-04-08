using Domain.Post.Entity;

namespace Domain.Category.Entity;

public sealed class CategoryEntity {
  public Guid Id { get; set; }
  public List<PostEntity> Posts { get; set; } = [];
  public string Category { get; set; }

  private CategoryEntity(Guid id, string category) {
    this.Id = id;
    this.Category = category;
  }

  public static CategoryEntity Create(Guid id, string category) {
    return new CategoryEntity(id, category);
  }
}
