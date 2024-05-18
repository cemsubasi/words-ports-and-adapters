using Domain.Category.Entity;
using Domain.File.Entity;

namespace Domain.Account.Entity;

public class AccountEntity {
  public Guid Id { get; private set; }
  public string Name { get; private set; }
  public string Email { get; private set; }
  public string Phone { get; private set; }
  public string Password { get; private set; }
  public string PasswordSalt { get; private set; }
  public DateTime CreatedAt { get; private set; }
  public DateTime UpdatedAt { get; private set; }
  public DateTime? DeletedAt { get; private set; }
  public Guid? CreatedBy { get; private set; }
  public AccountEntity Creator { get; private set; }
  public virtual List<CategoryEntity> Categories { get; set; } = [];
  public List<FileEntity> Files { get; private set; } = [];

  protected AccountEntity(Guid id, string name, string email, string phone, string password, string passwordSalt, Guid? createdBy = null) {
    this.Id = id;
    this.Name = name;
    this.Email = email;
    this.Phone = phone;
    this.Password = password;
    this.PasswordSalt = passwordSalt;
    this.CreatedAt = DateTime.UtcNow;
    this.UpdatedAt = DateTime.UtcNow;
    this.CreatedBy = createdBy;
  }

  public static AccountEntity Create(Guid id, string name, string email, string phone, string password, string passwordSalt, Guid? createdBy = null) {
    return new AccountEntity(id, name, email, phone, password, passwordSalt, createdBy);
  }
}
