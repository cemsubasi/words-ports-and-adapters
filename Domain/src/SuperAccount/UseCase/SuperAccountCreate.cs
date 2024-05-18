using Domain.Account.UseCase;

namespace Domain.SuperAccount.UseCase;

public class SuperAccountCreate(string name, string email, string phone, string password) : AccountCreate(name, email, phone, password) {
  public static new SuperAccountCreate Build(string name, string email, string phone, string password) {
    return new SuperAccountCreate(name, email, phone, password);
  }
}