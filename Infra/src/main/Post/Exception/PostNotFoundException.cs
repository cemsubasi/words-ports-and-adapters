namespace Infra.Account;

public class PostNotFoundException : UnauthorizedAccessException {
  /* private static string _message = "Missing or invalid credentials."; */

  public int StatusCode => (int)StatusCodes.Status401Unauthorized;

  public PostNotFoundException() : base() {
  }

  public PostNotFoundException(string message) : base(message) {
  }

  public static void ThrowIfNull(object obj, string message) {
    if (obj is null) {
      throw new PostNotFoundException(message);
    }
  }

  public static void ThrowIfFalse(bool cond, string message) {
    if (!cond) {
      throw new PostNotFoundException(message);
    }
  }

  public static void ThrowIfNull(object obj) {
    if (obj is null) {
      throw new PostNotFoundException();
    }
  }

  public static void ThrowIfFalse(bool cond) {
    if (!cond) {
      throw new PostNotFoundException();
    }
  }
}
