using Domain.Category.Entity;

namespace Infra.Category;

public class CategoryRetrieveResponse {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public int Count { get; set; }

  public static CategoryRetrieveResponse From(CategoryEntity entity) {
    return new CategoryRetrieveResponse {
      Id = entity.Id,
      Name = entity.Name,
      Count = entity.Questions.Count,
    };
  }

  public static CategoryRetrieveResponse[] From(CategoryEntity[] entities) {
    return entities.Select(x => new CategoryRetrieveResponse {
      Id = x.Id,
      Name = x.Name,
      Count = x.Questions.Count,
    }).ToArray();
  }
}
