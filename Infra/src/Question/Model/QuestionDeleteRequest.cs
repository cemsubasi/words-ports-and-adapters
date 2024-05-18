using Domain.Question.UseCase;

namespace Infra.Question;

public class QuestionDeleteRequest {
  public Guid Id { get; set; }

  public QuestionDelete ToUseCase(Guid accountId) {
    return QuestionDelete.Build(accountId, this.Id);
  }
}
