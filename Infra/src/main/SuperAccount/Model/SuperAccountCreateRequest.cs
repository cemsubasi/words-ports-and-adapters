using Domain.Account.UseCase;
using Domain.SuperAccount.UseCase;
using FluentValidation;
using Infra.SuperAccount.Validator;

namespace Infra.Controllers.SuperAccount;

public class SuperAccountCreateRequest(string name, string email, string phone, string password) {
  public string Name { get; private set; } = name;
  public string Email { get; private set; } = email;
  public string Phone { get; set; } = phone;
  public string Password { get; private set; } = password;

  public SuperAccountCreate ToUseCase() {
    var validationResult = new SuperAccountCreateRequestValidator().Validate(this);
    if (!validationResult.IsValid) {
      throw new Exception(validationResult.Errors.First().ErrorMessage);
    }

    return SuperAccountCreate.Build(this.Name, this.Email, this.Phone, this.Password);
  }
}
