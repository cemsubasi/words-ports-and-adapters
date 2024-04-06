using Domain.Category.Entity;
using Domain.Post.Entity;
using Domain.Post.UseCase;
using Mapster;

namespace Infra.Post;

public class PostMapper {
  public static void Map(TypeAdapterConfig config) {
    _ = config.NewConfig<CreatePost, PostEntity>()
      .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
      .Map(dest => dest.UpdatedAt, src => DateTime.UtcNow)
      .Ignore(x => x.Category);
  }
}
