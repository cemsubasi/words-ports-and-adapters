using Domain.Post.Entity;
using Infra.Comment.Model;

namespace Infra.Post.Model;

public class PostRetrieveResponse {
  public Guid Id { get; set; }
  public string Url { get; set; }
  public string Header { get; set; }
  public string Body { get; set; }
  public string Thumbnail { get; set; }
  public long CreatedAt { get; set; }
  public long UpdatedAt { get; set; }
  public ICollection<CommentRetrieveResponse> Comments { get; set; }

  public static PostRetrieveResponse From(PostEntity entity) {
    return new PostRetrieveResponse {
      Id = entity.Id,
      Url = entity.Url,
      Header = entity.Header,
      Body = entity.Body,
      Thumbnail = entity.Thumbnail,
      CreatedAt = entity.CreatedAt.ToUnixTimeSeconds(),
      UpdatedAt = entity.UpdatedAt.ToUnixTimeSeconds(),
      Comments = entity.Comments.Select(x => new CommentRetrieveResponse {
        Id = x.Id,
        Value = x.Comment,
      }).ToArray(),
    };
  }

  public static PostRetrieveResponse[] From(PostEntity[] entities) {
    return entities.Select(entity => new PostRetrieveResponse {
      Id = entity.Id,
      Url = entity.Url,
      Header = entity.Header,
      Body = entity.Body,
      Thumbnail = entity.Thumbnail,
      CreatedAt = entity.CreatedAt.ToUnixTimeSeconds(),
      UpdatedAt = entity.UpdatedAt.ToUnixTimeSeconds(),
      Comments = entity.Comments.Select(x => new CommentRetrieveResponse {
        Id = x.Id,
        Value = x.Comment,
      }).ToArray(),
    }).ToArray();
  }
}
