using Infra.Controllers.Common;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Infra;

public class HttpResponseExceptionFilter : IActionFilter {
  public void OnActionExecuted(ActionExecutedContext context) {
    switch (context.Exception) {
      case null:
        break;

      case UnauthorizedAccessException:
        context.Result = new UnauthorizedObjectResult(new ErrorResponse(context.Exception, context.HttpContext.TraceIdentifier)) {
          StatusCode = 401,
        };

        Sentry.SentrySdk.CaptureException(context.Exception, scope => scope.SetTag("TraceId", context.HttpContext.TraceIdentifier));
        context.ExceptionHandled = true;
        break;

      /* case ArgumentNullException: */
      /*   context.Result = new InternalServerErrorResult(new ErrorResponse(500, context.Exception.Message)); */
      /*   context.ExceptionHandled = true; */
      /*   break; */

      /* case DbUpdateException: */
      /*   context.Result = new InternalServerErrorResult(new ErrorResponse(500, context.Exception.Message)); */
      /*   context.ExceptionHandled = true; */
      /*   break; */

      default:
        context.Result = new ObjectResult(new ErrorResponse(context.Exception, context.HttpContext.TraceIdentifier)) {
          StatusCode = 500,
        };

        Sentry.SentrySdk.CaptureException(context.Exception, scope => scope.SetTag("TraceId", context.HttpContext.TraceIdentifier));
        context.ExceptionHandled = true;
        break;
    };
  }

  public void OnActionExecuting(ActionExecutingContext context) {
  }
}
