using Domain.Common;

using Microsoft.AspNetCore.Mvc;

namespace Infra.Controllers.Common;

public static class ControllerExtension {
  /* public static IActionResult Respond<T>(this ControllerBase controller, T item) { */
  /*   return controller.Ok(ActionResponse<T>.Build(item)); */
  /* } */

  public static IActionResult Respond<T>(this ControllerBase controller, DataResponse<T> data) {
    return controller.Ok(ActionResponse<DataResponse<T>>.Build(data));
  }

  /* public static IActionResult Respond<ErrorResponse>(this ControllerBase controller, ErrorResponse errorResponse) { */
  /*   return controller.StatusCode(StatusCodes.Status422UnprocessableEntity, ActionResponse<ErrorResponse>.Build(errorResponse)); */
  /* } */
}
