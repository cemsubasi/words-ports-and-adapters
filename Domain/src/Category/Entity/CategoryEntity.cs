using Domain.Account.Entity;
using Domain.Question.Entity;

namespace Domain.Category.Entity;

public class CategoryEntity {
  public Guid Id { get; private set; }
  public string Name { get; private set; }
  public Guid AccountId { get; private set; }
  public AccountEntity Account { get; private set; }
  public List<QuestionEntity> Questions { get; private set; } = [];

  private CategoryEntity(Guid id, string name, Guid accountId) {
    this.Id = id;
    this.Name = name;
    this.AccountId = accountId;
  }

  public static CategoryEntity Create(string name, Guid accountId) {
    return new CategoryEntity(Guid.NewGuid(), name, accountId);
  }
}
