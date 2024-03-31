using System.Security.Cryptography;
using Domain.Account.UseCase;

namespace Domain.SuperAccount.UseCase;

public class SuperAccountCreate : AccountCreate {
  public SuperAccountCreate(string name, string email, string phone, string password) : base(name, email, phone, password) {
  }

  public static new SuperAccountCreate Build(string name, string email, string phone, string password) {
    return new SuperAccountCreate(name, email, phone, password);
  }

}
