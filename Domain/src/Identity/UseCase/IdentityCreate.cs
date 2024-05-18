namespace Domain.Identity.UseCase;

public class IdentityCreate {
  private const string letters = "abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ0123456789";

  private IdentityCreate(string inetAddress, string userAgent, string language, Guid? accountId) {
    this.InetAddress = inetAddress;
    this.UserAgent = userAgent;
    this.Language = language;
    this.AccountId = accountId;
  }

  public string InetAddress { get; private set; }

  public string UserAgent { get; private set; }

  public string Language { get; private set; }

  public Guid? AccountId { get; private set; }

  public static IdentityCreate Build(string inetAddress, string userAgent, string language, Guid? accountId = null) {
    return new IdentityCreate(inetAddress, userAgent, language, accountId);
  }
}
