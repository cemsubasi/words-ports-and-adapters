using System.Text.Json;
using Serilog;

namespace Infra.Controllers.Common;

public class ErrorResponse {
  private string StackTrace { get; set; }

  public string TraceId { get; private set; }
  public string Message { get; private set; }
  public List<InnerResponse> InnerExceptions { get; private set; }

  public ErrorResponse(Exception exception, string traceId) {
    this.TraceId = traceId;
    this.Message = exception?.Message ?? "An error occurred";
    this.InnerExceptions = exception?.InnerException != null ? new List<InnerResponse> { new InnerResponse(exception.InnerException) } : new List<InnerResponse>();
    this.StackTrace = exception?.StackTrace ?? "No stack trace available";

    Log.Fatal(
      $@"Exception: {JsonSerializer.Serialize(this)}
       StackTrace: {this.StackTrace}");
  }

  public class InnerResponse(Exception innerException) {
    public string Message { get; set; } = innerException.Message;
  }
}
