using Domain.Account.Entity;

namespace Domain.Identity.Entity;

public class IdentityEntity {
  public uint Id { get; set; }

  public string InetAddress { get; set; }

  public string UserAgent { get; set; }

  public string Language { get; set; }

  public string Device { get; set; }

  public string Browser { get; set; }

  public string Os { get; set; }

  public Guid? AccountId { get; set; }

  public virtual AccountEntity Account { get; set; }
}
