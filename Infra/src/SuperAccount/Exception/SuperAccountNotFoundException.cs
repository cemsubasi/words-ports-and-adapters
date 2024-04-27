using Infra.Account;

namespace Infra.SuperAccount;

public class SuperAccountNotFoundException : AccountNotFoundException {
  public SuperAccountNotFoundException() : base() {
  }

  public SuperAccountNotFoundException(string message) : base(message) {
  }
}
