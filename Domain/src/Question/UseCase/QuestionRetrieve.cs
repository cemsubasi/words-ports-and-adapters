namespace Domain.Question.UseCase;

public class QuestionRetrieve {
  public Guid AccountId { get; set; }
  public Guid CategoryId { get; set; }

  public static QuestionRetrieve Build(Guid accountId, Guid categoryId) {
    return new QuestionRetrieve {
      AccountId = accountId,
      CategoryId = categoryId,
    };
  }
}
