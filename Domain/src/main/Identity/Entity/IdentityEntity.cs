using Domain.Account.Entity;

namespace Domain.Identity.Entity;

public sealed class IdentityEntity {
  public uint Id { get; private set; }

  public string InetAddress { get; private set; }

  public string UserAgent { get; private set; }

  public string Language { get; private set; }

  public Guid? AccountId { get; private set; }

  public AccountEntity Account { get; private set; }

  public DateTime CreatedAt { get; private set; }

  private IdentityEntity(string inetAddress, string userAgent, string language, Guid? accountId = null) {
    this.InetAddress = inetAddress;
    this.UserAgent = userAgent;
    this.Language = language;
    this.AccountId = accountId;
    this.CreatedAt = DateTime.UtcNow;
  }

  public static IdentityEntity Create(string inetAddress, string userAgent, string language, Guid? accountId = null) {
    return new IdentityEntity(inetAddress, userAgent, language, accountId);
  }
}
