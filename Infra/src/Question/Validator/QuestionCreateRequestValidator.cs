using FluentValidation;

namespace Infra.Question.Validator;

public class QuestionCreateRequestValidator : AbstractValidator<QuestionCreateRequest> {
  public QuestionCreateRequestValidator() {
    _ = this.RuleFor(x => x.Value)
      .NotNull()
      .NotEmpty()
      .MinimumLength(2)
      .WithMessage("Question must be valid.");

    _ = this.RuleFor(x => x.Answers)
      .NotNull()
      .NotEmpty()
      .ForEach(x => x.MinimumLength(2))
      .WithMessage("Answers must be valid.");
  }
}
