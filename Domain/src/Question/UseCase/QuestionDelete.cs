namespace Domain.Question.UseCase;

public class QuestionDelete {
  public Guid Id { get; private set; }
  public Guid AccountId { get; private set; }

  private QuestionDelete(Guid accountId, Guid id) {
    this.AccountId = accountId;
    this.Id = id;
  }

  public static QuestionDelete Build(Guid accountId, Guid id) {
    return new QuestionDelete(accountId, id);
  }
}
