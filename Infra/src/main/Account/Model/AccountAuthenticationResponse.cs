namespace Infra.Account;

public class AccountAuthenticationResponse {
  public string Token { get; set; }
  public long ExpireInMinutes { get; set; }

  public AccountAuthenticationResponse(string token, long expireInMinutes) {
    this.Token = token;
    this.ExpireInMinutes = expireInMinutes;
  }

  public static AccountAuthenticationResponse From(string token, long expireInMinutes) {
    return new AccountAuthenticationResponse(token, expireInMinutes);
  }
}
