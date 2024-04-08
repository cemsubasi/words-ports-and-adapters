using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Domain.Account.Entity;

using Infra.Context;
using Serilog;

namespace Infra.Middlewares;

public class SessionMiddleware(RequestDelegate next) {
  private readonly RequestDelegate next = next;

  public async Task InvokeAsync(HttpContext context, IUserSession session) {
    /* var userAgent = context.Request.Headers["User-Agent"].ToString(); */
    /* var browser = userAgent.Split(" ")[0]; */
    /* var operatingSystem = userAgent.Split(" ")[1]; */
    /* var device = userAgent.Split(" ")[2]; */
    /* var application = userAgent.Split(" ")[3]; */
    /* var ipAddress = context.Connection.RemoteIpAddress.ToString(); */
    /* var requestPath = context.Request.Path; */
    /* var requestMethod = context.Request.Method; */
    /* var requestQuery = context.Request.QueryString.ToString(); */
    /* var xForwardedFor = context.Request.Headers["X-Forwarded-For"].ToString(); */
    /* var host = context.Request.Host.ToString(); */
    /* var referer = context.Request.Headers["Referer"].ToString(); */
    /* var origin = context.Request.Headers["Origin"].ToString(); */

    /* Console.WriteLine($"Request: {requestMethod} {requestPath} {requestQuery}"); */
    /* Console.WriteLine($"User-Agent: {userAgent}"); */
    /* Console.WriteLine($"Browser: {browser}"); */
    /* Console.WriteLine($"Platform: {operatingSystem}"); */
    /* Console.WriteLine($"Device: {device}"); */
    /* Console.WriteLine($"Application: {application}"); */
    /* Console.WriteLine($"IP Address: {ipAddress}"); */
    /* Console.WriteLine($"X-Forwarded-For: {xForwardedFor}"); */
    /* Console.WriteLine($"Host: {host}"); */
    /* Console.WriteLine($"Referer: {referer}"); */
    /* Console.WriteLine($"Origin: {origin}"); */

    if (context.User.Identity.IsAuthenticated) {
      // Those are doesn't work when claim is "sub" but works when claim type is custom like "id"
      /* session.Id = Guid.Parse(context.User.Claims.FirstOrDefault(x => x.Type == "sub").Value); */
      /* session.Id = Guid.Parse(context.User.FindFirst("sub").Value); */
      /* session.Id = Guid.Parse($"{context.User.FindFirst("sub").Value}"); */
      /* session.Id = Guid.Parse($"{context.User.FindFirstValue(ClaimTypes.NameIdentifier)}"); */

      var isClaimReadable = Guid.TryParse(context.User.FindFirstValue(ClaimTypes.NameIdentifier), out Guid userId);
      if (!isClaimReadable) {
        throw new InvalidOperationException("Claim could not be read");
      }

      session.Id = userId;
    }

    await this.next.Invoke(context);
  }
}
