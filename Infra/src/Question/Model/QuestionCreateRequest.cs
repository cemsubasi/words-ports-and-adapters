using System.Text.Json.Serialization;
using Domain.Question.UseCase;

namespace Infra.Question;

public class QuestionCreateRequest {
  [JsonIgnore]
  public Guid AccountId { get; set; }
  public Guid CategoryId { get; set; }
  public string Value { get; set; }
  public string[] Answers { get; set; }

  public QuestionCreate ToUseCase(Guid accountId) {
    return QuestionCreate.Build(accountId: accountId, categoryId: this.CategoryId, @value: this.Value, answers: this.Answers);
  }
}
