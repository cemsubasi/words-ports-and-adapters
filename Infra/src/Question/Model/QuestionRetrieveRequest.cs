using System.Text.Json.Serialization;
using Domain.Question.UseCase;

namespace Infra.Question;

public class QuestionRetrieveRequest {
  [JsonIgnore]
  public Guid AccountId { get; set; }
  public Guid CategoryId { get; set; }

  public QuestionRetrieve ToUseCase(Guid accountId) {
    return new QuestionRetrieve {
      AccountId = accountId,
      CategoryId = this.CategoryId,
    };
  }
}
