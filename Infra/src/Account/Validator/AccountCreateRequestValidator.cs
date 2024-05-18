using FluentValidation;
using Infra.Controllers.Account;

namespace Infra.Account.Validator;

public class AccountCreateRequestValidator : AbstractValidator<AccountCreateRequest> {
  public AccountCreateRequestValidator() {
    _ = this.RuleFor(x => x.Email)
      .EmailAddress();

    _ = this.RuleFor(x => x.Phone)
      .NotNull()
      .NotEmpty()
      .Length(11)
      .WithMessage("Phone must be valid.");

    _ = this.RuleFor(x => x.Password)
      .NotNull()
      .NotEmpty()
      .MinimumLength(6)
      .MaximumLength(32);
  }
}
