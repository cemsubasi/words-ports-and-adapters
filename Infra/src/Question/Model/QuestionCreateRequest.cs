using System.Text.Json.Serialization;
using Domain.Question.UseCase;
using Infra.Question.Validator;

namespace Infra.Question;

public class QuestionCreateRequest {
  [JsonIgnore]
  public Guid AccountId { get; set; }
  public Guid CategoryId { get; set; }
  public string Value { get; set; }
  public string[] Answers { get; set; }

  public QuestionCreate ToUseCase(Guid accountId) {
    var validationResult = new QuestionCreateRequestValidator().Validate(this);
    if (!validationResult.IsValid) {
      throw new Exception(validationResult.Errors.First().ErrorMessage);
    }

    return QuestionCreate.Build(accountId: accountId, categoryId: this.CategoryId, @value: this.Value, answers: this.Answers);
  }
}
