using Domain.Question.Entity;

namespace Domain.Answer.Entity;

public class AnswerEntity {
  public Guid Id { get; private set; }
  public Guid QuestionId { get; private set; }
  public QuestionEntity Question { get; private set; }
  public string Value { get; private set; }

  private AnswerEntity(Guid id, Guid questionId, string value) {
    this.Id = id;
    this.QuestionId = questionId;
    this.Value = value;
  }

  public static AnswerEntity Create(Guid questionId, string value) {
    return new AnswerEntity(Guid.NewGuid(), questionId, value);
  }
}
