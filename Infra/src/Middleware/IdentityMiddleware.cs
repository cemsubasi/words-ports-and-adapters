using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;
using Domain.Account.Entity;
using Domain.Identity.Port;
using Domain.Identity.UseCase;
using Infra.Context;
using Serilog;

namespace Infra.Middlewares;

public class IdentityMiddleware(RequestDelegate next) {
  private readonly RequestDelegate next = next;

  public async Task InvokeAsync(HttpContext context, IUserSession session, IdentityPort identityPort) {
    if (context.User.Identity.IsAuthenticated) {
      // Those are doesn't work when claim is "sub" but works when claim type is custom like "id"
      /* session.Id = Guid.Parse(context.User.Claims.FirstOrDefault(x => x.Type == "sub").Value); */
      /* session.Id = Guid.Parse(context.User.FindFirst("sub").Value); */
      /* session.Id = Guid.Parse($"{context.User.FindFirst("sub").Value}"); */
      /* session.Id = Guid.Parse($"{context.User.FindFirstValue(ClaimTypes.NameIdentifier)}"); */

      var isClaimReadable = Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out var userId);
      if (!isClaimReadable) {
        throw new InvalidOperationException("Claim could not be read");
      }

      session.Id = userId;
    }

    var inetAddress = context.Connection.RemoteIpAddress?.ToString();
    var userAgent = context.Request.Headers.UserAgent.ToString();
    var language = context.Request.Headers.AcceptLanguage.ToString();
    var accountId = Guid.TryParse(session.Id.ToString(), out var id) && id != Guid.Empty ? id : (Guid?)null;
    var identityCreate = IdentityCreate.Build(inetAddress, userAgent, language, null);
    _ = await identityPort.CreateAsync(identityCreate, CancellationToken.None);

    await this.next.Invoke(context);
  }
}
