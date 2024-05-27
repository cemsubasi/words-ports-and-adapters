using System.Security.Cryptography;
using Domain.Question.Entity;
using Domain.Question.Port;
using Domain.Question.UseCase;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Question.Adapter;

public class QuestionAdapter : QuestionPort {
  private readonly MainDbContext context;

  public QuestionAdapter(MainDbContext context) => this.context = context;

  public async Task CreateAsync(QuestionCreate useCase, CancellationToken cancellationToken) {
    var category = await this.context.Categories
      .Include(x => x.Questions)
      .ThenInclude(x => x.Answers)
      .SingleOrDefaultAsync(x => x.AccountId == useCase.AccountId && x.Id == useCase.CategoryId, cancellationToken);

    if (category is null) {
      throw new Exception($"Category not found with id: {useCase.CategoryId}");
    }

    var questionEntity = QuestionEntity.Create(useCase.CategoryId, useCase.Value);
    _ = await this.context.Questions.AddAsync(questionEntity, cancellationToken);
    category.Questions.Add(questionEntity);
    this.context.Categories.Update(category);
    var result = await this.context.SaveChangesAsync(cancellationToken);
    if (result.Equals(0)) {
      throw new Exception("An exception occoured while saving question entity");
    }
  }

  public async Task DeleteAsync(QuestionDelete useCase, CancellationToken cancellationToken) {
    var question = await this.context.Questions
      .SingleOrDefaultAsync(x => x.Id == useCase.Id && x.Category.AccountId == useCase.AccountId, cancellationToken);

    if (question is null) {
      throw new InvalidOperationException($"Question not found with Id: {useCase.Id}");
    }

    _ = this.context.Questions.Remove(question);
    var result = await this.context.SaveChangesAsync(cancellationToken);
    if (result.Equals(0)) {
      throw new Exception("An exception occoured while saving question entity");
    }
  }

  public async Task<QuestionEntity[]> RetrieveAsync(QuestionRetrieve useCase, CancellationToken cancellationToken) {
    var questions = await this.context.Questions
      .Where(x => x.CategoryId == useCase.CategoryId && x.Category.AccountId == useCase.AccountId)
      .Include(x => x.Answers)
      .ToArrayAsync(cancellationToken);

    return questions;
  }
}
