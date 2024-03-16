using Infra.Controllers.Common;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Infra;

public class HttpResponseExceptionFilter : IActionFilter {
  public void OnActionExecuted(ActionExecutedContext context) {
    switch (context.Exception) {
      case UnauthorizedAccessException:
        context.Result = new UnauthorizedObjectResult(ActionResponse<ErrorResponse>.Build(new ErrorResponse(401, context.Exception.Message)));
        context.ExceptionHandled = true;
        break;

      case ArgumentNullException:
        context.Result = new InternalServerErrorResult(new ErrorResponse(500, context.Exception.Message));
        context.ExceptionHandled = true;
        break;
    };

    /* if (context.Exception is UnauthorizedAccessException e) { */
    /*   context.Result = new UnauthorizedObjectResult(ActionResponse<ErrorResponse>.Build(new ErrorResponse(401, e.Message))); */
    /*   context.ExceptionHandled = true; */
    /* } */
  }

  public void OnActionExecuting(ActionExecutingContext context) {
  }
}
