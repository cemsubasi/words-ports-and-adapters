using Domain.Question.Entity;

namespace Infra.Question;

public class QuestionRetrieveResponse {
  public Guid CategoryId { get; set; }
  public Guid Id { get; set; }
  public string Value { get; set; }
  public string[] Answers { get; set; }

  public static QuestionRetrieveResponse[] From(QuestionEntity[] entities) {
    return entities.Select(x => new QuestionRetrieveResponse {
      CategoryId = x.CategoryId,
      Id = x.Id,
      Value = x.Value,
      Answers = x.Answers.Select(x => x.Value).ToArray(),
    }).ToArray();
  }
}
