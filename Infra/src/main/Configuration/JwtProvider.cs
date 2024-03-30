using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace Infra.Configurations;

public class JwtProvider : IJwtProvider {
  private readonly JwtOptions options;
  private DateTimeOffset expireTimeOffset => DateTimeOffset.UtcNow.AddDays(7);
  private DateTime notBeforeTime => DateTimeOffset.UtcNow.DateTime;

  public JwtProvider(IOptions<JwtOptions> options) {
    this.options = options.Value;
  }

  public (string, long) Generate(Guid id, Claim[] claims = null) {
    var expireAt = this.expireTimeOffset.ToUnixTimeMilliseconds();
    var signinCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecretKey)), SecurityAlgorithms.HmacSha512);

    var token = new JwtSecurityToken(
      options.Issuer,
      options.Audience,
      claims,
      notBeforeTime,
      expireTimeOffset.LocalDateTime,
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
