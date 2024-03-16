namespace Domain.Post.UseCase;

public class CreatePost {
  public Guid AccountId { get; set; }
  public Guid? CategoryId { get; set; }
  public string CategoryName { get; set; }
  public string Url { get; set; }
  public string Header { get; set; }
  public string Body { get; set; }
  public string Thumbnail { get; set; }
  public bool IsFeatured { get; set; }

  private CreatePost(Guid accountId, Guid? categoryId, string categoryName, string url, string header, string body, string thumbnail, bool isFeatured) {
    this.AccountId = accountId;
    this.CategoryId = categoryId;
    this.CategoryName = categoryName;
    this.Url = url;
    this.Header = header;
    this.Body = body;
    this.Thumbnail = thumbnail;
    this.IsFeatured = isFeatured;
  }

  public static CreatePost Build(Guid accountId, Guid? categoryId, string categoryName, string url, string header, string body, string thumbnail, bool isFeatured) {
    return new CreatePost(accountId, categoryId, categoryName, url, header, body, thumbnail, isFeatured);
  }
}
