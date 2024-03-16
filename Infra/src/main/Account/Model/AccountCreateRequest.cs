using Domain.Account.UseCase;
using FluentValidation;
using Infra.Account.Validator;

namespace Infra.Controllers.Account;

public class AccountCreateRequest {
  public string Name { get; private set; }
  public string Email { get; private set; }
  public string Phone { get; set; }
  public string Password { get; private set; }

  public AccountCreateRequest(string name, string email, string phone, string password) {
    this.Name = name;
    this.Email = email;
    this.Phone = phone;
    this.Password = password;
  }

  public AccountCreate ToUseCase() {
    var validationResult = new AccountCreateRequestValidator().Validate(this);
    if (!validationResult.IsValid) {
      throw new Exception(validationResult.Errors.First().ErrorMessage);
    }

    return AccountCreate.Build(this.Name, this.Email, this.Phone, this.Password);
  }
}
