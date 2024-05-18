using Domain.Question.Entity;
using Domain.Question.UseCase;

namespace Domain.Question.Port;

public interface QuestionPort {
  Task CreateAsync(QuestionCreate useCase, CancellationToken cancellationToken);
  Task<QuestionEntity[]> RetrieveAsync(QuestionRetrieve useCase, CancellationToken cancellationToken);
  Task DeleteAsync(QuestionDelete useCase, CancellationToken cancellationToken);
}
