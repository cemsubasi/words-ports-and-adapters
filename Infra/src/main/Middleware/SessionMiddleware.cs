using Domain.Account.Entity;

using Infra.Context;

namespace Infra.Middlewares;

public class SessionMiddleware {
  private readonly RequestDelegate next;
  public SessionMiddleware(RequestDelegate next) => this.next = next;

  public async Task InvokeAsync(HttpContext context, IUserSession session) {
    var userId = context.User.FindFirst("id")?.Value;
    if (!string.IsNullOrEmpty(userId)) {
      var parseResult = Guid.TryParse(userId, out Guid value);
      if (parseResult) {
        session.Id = value;
      }
    }

    Console.WriteLine(context.GetEndpoint());

    await next.Invoke(context);
  }
}
