using Domain.Account.UseCase;

namespace Infra.Account;

public class AccountAuthenticationRequest {
  public string Email { get; set; }
  public string Password { get; set; }

  public AccountAuthenticationRequest() {

  }

  private AccountAuthenticationRequest(string email, string password) {
    this.Email = email;
    this.Password = password;
  }

  public AccountAuthenticate ToUseCase() {
    return AccountAuthenticate.Build(this.Email, this.Password);
  }
}
