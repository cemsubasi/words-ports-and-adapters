using Domain.Account.UseCase;
using Infra.Account.Validator;

namespace Infra.Controllers.Account;

public class AccountCreateRequest(string name, string email, string phone, string password) {
  public string Name { get; private set; } = name;
  public string Email { get; private set; } = email;
  public string Phone { get; set; } = phone;
  public string Password { get; private set; } = password;

  public AccountCreate ToUseCase() {
    var validationResult = new AccountCreateRequestValidator().Validate(this);
    if (!validationResult.IsValid) {
      throw new Exception(validationResult.Errors.First().ErrorMessage);
    }

    return AccountCreate.Build(this.Name, this.Email, this.Phone, this.Password);
  }
}
