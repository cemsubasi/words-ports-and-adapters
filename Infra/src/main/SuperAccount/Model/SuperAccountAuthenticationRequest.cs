using Domain.SuperAccount.UseCase;

namespace Infra.SuperAccount;

public class SuperAccountAuthenticationRequest {
  public string Email { get; set; }
  public string Password { get; set; }

  public SuperAccountAuthenticationRequest() {

  }

  private SuperAccountAuthenticationRequest(string email, string password) {
    this.Email = email;
    this.Password = password;
  }

  public SuperAccountAuthenticate ToUseCase() {
    return SuperAccountAuthenticate.Build(this.Email, this.Password);
  }
}
