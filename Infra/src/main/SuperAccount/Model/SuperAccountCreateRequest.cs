using Domain.Account.UseCase;
using Domain.SuperAccount.UseCase;
using FluentValidation;
using Infra.SuperAccount.Validator;

namespace Infra.Controllers.SuperAccount;

public class SuperAccountCreateRequest {
  public string Name { get; private set; }
  public string Email { get; private set; }
  public string Phone { get; set; }
  public string Password { get; private set; }

  public SuperAccountCreateRequest(string name, string email, string phone, string password) {
    this.Name = name;
    this.Email = email;
    this.Phone = phone;
    this.Password = password;
  }

  public SuperAccountCreate ToUseCase() {
    var validationResult = new SuperAccountCreateRequestValidator().Validate(this);
    if (!validationResult.IsValid) {
      throw new Exception(validationResult.Errors.First().ErrorMessage);
    }

    return AccountCreate.Build(this.Name, this.Email, this.Phone, this.Password) as SuperAccountCreate;
  }
}
