using System.Security.Cryptography;

namespace Domain.Account.UseCase;

public class AccountAuthenticate {
  public string Email { get; set; }
  public string Password { get; set; }

  public AccountAuthenticate()
  {

  }

  private AccountAuthenticate(string email, string password) {
    this.Email = email;
    this.Password = password;
  }

  public static AccountAuthenticate Build(string email, string password) {
    return new AccountAuthenticate(email, password);
  }

  public string GenerateHash(string password, string passwordSalt) {
    byte[] data = System.Text.Encoding.UTF8.GetBytes(password + passwordSalt);
    byte[] hash = SHA256.Create().ComputeHash(data);
    var result = Convert.ToBase64String(hash);

    return result;
  }
}
