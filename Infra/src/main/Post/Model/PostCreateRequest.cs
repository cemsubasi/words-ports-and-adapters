using Domain.Post.UseCase;

namespace Infra.Post.Model;

public class PostCreateRequest {
  public Guid? CategoryId { get; set; }
  public string Category { get; set; }
  public string Url { get; set; }
  public string Header { get; set; }
  public string Body { get; set; }
  public string Thumbnail { get; set; }
  public bool IsFeatured { get; set; }

  public CreatePost ToUseCase(Guid accountId) {
    return CreatePost.Build(accountId, this.CategoryId, this.Category, this.Url, this.Header, this.Body, this.Thumbnail, this.IsFeatured);
  }
}
