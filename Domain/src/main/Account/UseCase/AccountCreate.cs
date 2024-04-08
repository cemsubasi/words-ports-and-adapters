using System.Security.Cryptography;

namespace Domain.Account.UseCase;

public class AccountCreate {
  private const string letters = "abcdefghijklmnopqrstuvxwyzABCDEFGHIJKLMNOPQRSTUVXWYZ0123456789";

  public AccountCreate(string name, string email, string phone, string password) {
    this.Name = name;
    this.Email = email;
    this.Phone = phone;
    this.PasswordSalt = GenerateSalt();
    this.Password = GenerateHash(password, this.PasswordSalt);
  }

  public string Name { get; private set; }
  public string Email { get; private set; }
  public string Password { get; private set; }
  public string PasswordSalt { get; set; }
  public string Phone { get; set; }


  public static AccountCreate Build(string name, string email, string phone, string password) {
    return new AccountCreate(name, email, phone, password);
  }

  public static string GenerateSalt(int digit = 8) {
    var hash = string.Empty;

    for (var i = 0; i < digit; i++) {
      var rnd = Random.Shared.Next(0, letters.Length);
      hash += letters.AsEnumerable().ElementAt(rnd);
    }

    return hash;
  }

  public static string GenerateHash(string password, string passwordSalt) {
    byte[] data = System.Text.Encoding.UTF8.GetBytes(password + passwordSalt);
    byte[] hash = SHA256.HashData(data);
    var result = Convert.ToBase64String(hash);

    return result;
  }
}
