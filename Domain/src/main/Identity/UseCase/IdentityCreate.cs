using System.Security.Cryptography;

namespace Domain.Identity.UseCase;

public class IdentityCreate {
  private const string letters = "abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ0123456789";

  public IdentityCreate(string name, string email, string phone, string password) {
    this.Name = name;
    this.Email = email;
    this.Phone = phone;
  }

  public string Name { get; private set; }
  public string Email { get; private set; }
  public string Password { get; private set; }
  public string PasswordSalt { get; set; }
  public string Phone { get; set; }


  public static IdentityCreate Build(string name, string email, string phone, string password) {
    return new IdentityCreate(name, email, phone, password);
  }
}
