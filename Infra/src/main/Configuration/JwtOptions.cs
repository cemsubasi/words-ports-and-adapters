namespace Infra.Configurations;

public class JwtOptions {
  public string Audience { get; init; }
  public string Issuer { get; init; }
  public string SecretKey { get; init; }
}
