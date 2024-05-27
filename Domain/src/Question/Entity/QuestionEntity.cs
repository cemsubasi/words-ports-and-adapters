using Domain.Answer.Entity;
using Domain.Category.Entity;

namespace Domain.Question.Entity;

public class QuestionEntity {
  public Guid Id { get; private set; }
  public Guid CategoryId { get; private set; }
  public CategoryEntity Category { get; private set; }
  public List<AnswerEntity> Answers { get; private set; } = [];
  public string Value { get; private set; }

  private QuestionEntity(Guid id, Guid categoryId, string value) {
    this.Id = id;
    this.CategoryId = categoryId;
    this.Value = value;
  }

  public static QuestionEntity Create(Guid categoryId, string value) {
    var questionId = Guid.NewGuid();
    return new QuestionEntity(questionId, categoryId, value);
  }

  public void AddAnswer(string value) {
    this.Answers.Add(AnswerEntity.Create(this.Id, value));
  }

  public void AddAnswer(string[] value) {
    this.Answers.AddRange(value.Select(x => AnswerEntity.Create(this.Id, x)));
  }
}
