using Infra.Controllers.Common;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Infra;

public class InternalServerErrorResult(ErrorResponse errorResponse) : StatusCodeResult(500) {
  public ErrorResponse Errors { get; set; } = errorResponse;
}
