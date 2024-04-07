using Domain.Comment.Entity;

namespace Infra.Comment.Model;

public class CommentRetrieveResponse {
  public Guid PostId { get; set; }
  public Guid Id { get; set; }
  public string Value { get; set; }
  public ICollection<CommentRetrieveResponse> SubComments { get; set; }

  public static CommentRetrieveResponse From(CommentEntity entity) {
    if (entity is null) {
      return null;
    }

    return new CommentRetrieveResponse {
      PostId = entity.PostId,
      Id = entity.Id,
      Value = entity.Comment,
      SubComments = entity.SubComments?.Select(subCommentEntity => CommentRetrieveResponse.From(subCommentEntity)).ToArray() ?? Array.Empty<CommentRetrieveResponse>(),
    };
  }

  public static CommentRetrieveResponse[] From(CommentEntity[] entities) {
    return entities.Select(entity => CommentRetrieveResponse.From(entity)).ToArray();
  }
}
