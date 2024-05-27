using Domain.Question.Port;
using Domain.Question.UseCase;

namespace Domain.Question;

public class QuestionCreateUseCaseHandler {
  private readonly QuestionPort questionPort;

  public QuestionCreateUseCaseHandler(QuestionPort questionPort) => this.questionPort = questionPort;

  public async Task Handle(QuestionCreate useCase, CancellationToken cancellationToken) {
    await this.questionPort.CreateAsync(useCase, cancellationToken);
  }
}
