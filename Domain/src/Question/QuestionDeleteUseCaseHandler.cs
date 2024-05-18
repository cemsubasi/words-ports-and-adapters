using Domain.Question.Port;
using Domain.Question.UseCase;

namespace Domain.Question;

public class QuestionDeleteUseCaseHandler {
  private readonly QuestionPort questionPort;

  public QuestionDeleteUseCaseHandler(QuestionPort questionPort) {
    this.questionPort = questionPort;
  }

  public async Task Handle(QuestionDelete useCase, CancellationToken cancellationToken) {
    await this.questionPort.DeleteAsync(useCase, cancellationToken);
  }
}
