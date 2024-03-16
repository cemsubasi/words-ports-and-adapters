namespace Infra.Controllers.Common;

public class ErrorResponse {
  public int errorCode { get; set; }
  public string errorMessage { get; set; }

  public ErrorResponse(int errorCode, string errorMessage) {
    this.errorCode = errorCode;
    this.errorMessage = errorMessage;
  }
}
