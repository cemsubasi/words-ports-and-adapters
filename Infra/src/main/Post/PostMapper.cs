using Domain.Category.Entity;
using Domain.Post.Entity;
using Domain.Post.UseCase;
using Mapster;

namespace Infra.Post;

public class PostMapper {
  public static void Map(TypeAdapterConfig config) {
    _ = config.NewConfig<CreatePost, PostEntity>()
      .Ignore(x => x.Category);
  }
}
