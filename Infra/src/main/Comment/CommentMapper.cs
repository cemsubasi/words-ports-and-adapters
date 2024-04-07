using Domain.Comment.Entity;
using Domain.Comment.UseCase;
using Mapster;

namespace Infra.Comment;

public class CommentMapper {
  public static void Map(TypeAdapterConfig config) {
    _ = config.NewConfig<CreateComment, CommentEntity>()
      .Map(dest => dest.CreatedAt, src => DateTimeOffset.UtcNow)
      .Map(dest => dest.CreatedBy, src => src.AccountId, cond => cond.AccountId.HasValue)
      .Map(dest => dest.PostId, src => src.PostId)
      .Map(dest => dest.ParentCommentId, src => src.ParentCommentId)
      .Map(dest => dest.Comment, src => src.Comment);
  }
}
