using Infra.Common;

namespace Infra.Account;

public class AccountNotFoundException : UnauthorizedAccessException {
  /* private static string _message = "Missing or invalid credentials."; */

  public int StatusCode => (int)StatusCodes.Status401Unauthorized;

  public AccountNotFoundException() {
  }

  public AccountNotFoundException(string message) {
  }

  public static void ThrowIfNull(object obj, string message) {
    if (obj is null) {
      throw new AccountNotFoundException(message);
    }
  }

  public static void ThrowIfFalse(bool cond, string message) {
    if (!cond) {
      throw new AccountNotFoundException(message);
    }
  }

  public static void ThrowIfNull(object obj) {
    if (obj is null) {
      throw new AccountNotFoundException();
    }
  }

  public static void ThrowIfFalse(bool cond) {
    if (!cond) {
      throw new AccountNotFoundException();
    }
  }
}
