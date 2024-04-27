using System.Security.Claims;

namespace Infra.Configurations;

public interface IJwtProvider {
  (string, long) Generate(Guid id, Claim[] claims);
}
