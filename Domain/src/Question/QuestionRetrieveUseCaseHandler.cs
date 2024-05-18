using Domain.Question.Entity;
using Domain.Question.Port;
using Domain.Question.UseCase;

namespace Domain.Question;

public class QuestionRetrieveUseCaseHandler {
  private readonly QuestionPort questionPort;
  public QuestionRetrieveUseCaseHandler(QuestionPort questionPort) {
    this.questionPort = questionPort;
  }

  public async Task<QuestionEntity[]> Handle(QuestionRetrieve useCase, CancellationToken cancellationToken) {
    var result = await this.questionPort.RetrieveAsync(useCase, cancellationToken);

    return result;
  }
}
