using Domain.Post.Entity;

namespace Domain.Account.Entity;

public class AccountEntity {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Email { get; set; }
  public string Phone { get; set; }
  public string Password { get; set; }
  public string PasswordSalt { get; set; }
  public ICollection<PostEntity> Posts { get; set; }
}
