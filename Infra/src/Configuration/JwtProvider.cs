using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infra.Configurations;

public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider {
  private readonly JwtOptions options = options.Value;
  private DateTimeOffset expireTimeOffset => DateTimeOffset.UtcNow.AddDays(7);
  private DateTime notBeforeTime => DateTimeOffset.UtcNow.DateTime;

  public (string, long) Generate(Guid id, Claim[] claims = null) {
    var expireAt = this.expireTimeOffset.ToUnixTimeMilliseconds();
    var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.SecretKey)), SecurityAlgorithms.HmacSha512);

    var token = new JwtSecurityToken(
      this.options.Issuer,
      this.options.Audience,
      claims,
      this.notBeforeTime,
      this.expireTimeOffset.LocalDateTime,
      signinCredentials);
    var tokenHandler = new JwtSecurityTokenHandler();
    var tokenValue = tokenHandler.WriteToken(token);

    return (tokenValue, expireAt);
  }

  public static TokenValidationParameters GetValidationParameters(string[] args, IConfigurationRoot configuration) {
    var options = configuration.GetSection("Jwt").Get<JwtOptions>();

    string audience = options.Audience;
    string issuer = options.Issuer;
    string secretKey = options.SecretKey;
    var key = Encoding.ASCII.GetBytes(secretKey);
    return new TokenValidationParameters() {
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateIssuerSigningKey = true,
      ValidIssuer = issuer,
      ValidAudience = audience,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
  }
}
