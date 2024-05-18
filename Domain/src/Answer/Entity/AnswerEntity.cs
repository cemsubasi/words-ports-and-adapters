using Domain.Question.Entity;

namespace Domain.Answer.Entity;

public class AnswerEntity {
  public Guid Id { get; set; }
  public Guid QuestionId { get; set; }
  public QuestionEntity Question { get; set; }
  public string Value { get; set; }
}
