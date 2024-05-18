namespace Domain.Question.UseCase;

public class QuestionCreate {
  public Guid AccountId { get; set; }
  public Guid CategoryId { get; set; }
  public string Value { get; set; }
  public string[] Answers { get; set; }

   public static QuestionCreate Build(Guid accountId, Guid categoryId, string @value, string[] answers) {
     return new QuestionCreate {
       AccountId = accountId,
       CategoryId = categoryId,
       Value = @value,
       Answers = answers,
    };
   }
}
