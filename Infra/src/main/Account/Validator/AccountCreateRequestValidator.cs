using FluentValidation;
using Infra.Controllers.Account;

namespace Infra.Account.Validator;

public class AccountCreateRequestValidator : AbstractValidator<AccountCreateRequest> {
  public AccountCreateRequestValidator() {
    RuleFor(x => x.Email)
      .EmailAddress();

    RuleFor(x => x.Phone)
      .NotNull()
      .NotEmpty()
      .Length(11)
      .WithMessage("Phone must be valid.");

    RuleFor(x => x.Password)
      .NotNull()
      .NotEmpty()
      .MinimumLength(6)
      .MaximumLength(32);
  }
}
