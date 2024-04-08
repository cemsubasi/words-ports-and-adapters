using Domain.SuperAccount.UseCase;

namespace Infra.SuperAccount;

public class SuperAccountAuthenticationRequest(string email, string password) {
  public string Email { get; set; } = email;
  public string Password { get; set; } = password;

  public SuperAccountAuthenticate ToUseCase() {
    return SuperAccountAuthenticate.Build(this.Email, this.Password);
  }
}
