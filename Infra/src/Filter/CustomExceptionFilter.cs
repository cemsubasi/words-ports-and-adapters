using Microsoft.AspNetCore.Mvc.Filters;

namespace Infra.Filters;

public class CustomExceptionFilter : ExceptionFilterAttribute {
  public override void OnException(ExceptionContext context) {
    /* var message = context.Exception.Message; */

    /* var error = new { */
    /*   message, */
    /*   hasError = true, */
    /* }; */

    /* if (context.Exception is IException exception) { */
    /*   context.Result = new UnauthorizedObjectResult(ActionResponse<ErrorResponse>.Build(new ErrorResponse(401, context.Exception.Message))); */
    /* } else { */
    /*   context.Result = new InternalServerErrorResult(new ErrorResponse(500, context.Exception.Message)); */
    /*   context.ExceptionHandled = true; */
    /* } */
  }
}
