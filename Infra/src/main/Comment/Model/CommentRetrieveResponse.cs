namespace Infra.Comment.Model;

public class CommentRetrieveResponse {
  public Guid PostId { get; set; }
  public Guid Id { get; set; }
  public string Value { get; set; }
  public ICollection<CommentRetrieveResponse> SubComments { get; set; }
}
