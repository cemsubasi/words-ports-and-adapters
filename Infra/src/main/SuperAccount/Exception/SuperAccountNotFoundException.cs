using Infra.Account;
using Infra.Common;

namespace Infra.SuperAccount;

public class SuperAccountNotFoundException : AccountNotFoundException {
  /* private static string _message = "Missing or invalid credentials."; */

  public SuperAccountNotFoundException() : base() {
  }

  public SuperAccountNotFoundException(string message) : base(message) {
  }
}
