using Infra.Controllers.Common;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Infra;

public class InternalServerErrorResult : StatusCodeResult {
  public ErrorResponse Errors { get; set; }
  public InternalServerErrorResult(ErrorResponse errorResponse) : base(500) {
    this.Errors = errorResponse;
  }
}
