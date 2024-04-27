using Domain.Account.UseCase;

namespace Infra.Account;

public class AccountAuthenticationRequest(string email, string password) {
  public string Email { get; set; } = email;
  public string Password { get; set; } = password;

  public AccountAuthenticate ToUseCase() {
    return AccountAuthenticate.Build(this.Email, this.Password);
  }
}
