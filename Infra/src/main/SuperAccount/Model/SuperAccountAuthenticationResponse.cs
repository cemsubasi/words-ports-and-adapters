namespace Infra.SuperAccount;

public class SuperAccountAuthenticationResponse {
  public string Token { get; set; }
  public long ExpireInMinutes { get; set; }

  public SuperAccountAuthenticationResponse(string token, long expireInMinutes) {
    this.Token = token;
    this.ExpireInMinutes = expireInMinutes;
  }

  public static SuperAccountAuthenticationResponse From(string token, long expireInMinutes) {
    return new SuperAccountAuthenticationResponse(token, expireInMinutes);
  }
}
