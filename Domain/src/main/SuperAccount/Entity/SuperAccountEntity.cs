using Domain.Account.Entity;

namespace Domain.SuperAccount.Entity;

public sealed class SuperAccountEntity : AccountEntity {
  private SuperAccountEntity(Guid id, string name, string email, string phone, string password, string passwordSalt, Guid? createdBy = null) : base(id, name, email, phone, password, passwordSalt, createdBy = null) {
  }
}
