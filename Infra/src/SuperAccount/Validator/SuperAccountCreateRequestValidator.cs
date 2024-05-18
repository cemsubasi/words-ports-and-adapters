using FluentValidation;
using Infra.Controllers.SuperAccount;

namespace Infra.SuperAccount.Validator;

public class SuperAccountCreateRequestValidator : AbstractValidator<SuperAccountCreateRequest> {
  public SuperAccountCreateRequestValidator() {
    this.RuleFor(x => x.Email)
      .EmailAddress();

    this.RuleFor(x => x.Phone)
      .NotNull()
      .NotEmpty()
      .Length(11)
      .WithMessage("Phone must be valid.");

    this.RuleFor(x => x.Password)
      .NotNull()
      .NotEmpty()
      .MinimumLength(6)
      .MaximumLength(32);
  }
}
