using System.Security.Cryptography;
using Domain.Account.UseCase;

namespace Domain.SuperAccount.UseCase;

public class SuperAccountAuthenticate : AccountAuthenticate {
  private SuperAccountAuthenticate(string email, string password) {
    this.Email = email;
    this.Password = password;
  }

  public static new SuperAccountAuthenticate Build(string email, string password) {
    return new SuperAccountAuthenticate(email, password);
  }
}
