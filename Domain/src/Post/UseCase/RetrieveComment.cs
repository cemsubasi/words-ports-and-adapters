namespace Domain.Comment.UseCase;

public class RetrieveComment {
  public Guid Id { get; private set; }

  private RetrieveComment(Guid id) {
    this.Id = id;
  }

  public static RetrieveComment Build(Guid id) {
    return new RetrieveComment(id);
  }
}
