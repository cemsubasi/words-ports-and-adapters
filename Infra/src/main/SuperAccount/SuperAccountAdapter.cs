using System.Security.Claims;
using System.Security.Cryptography;
using Domain.Common;
using Domain.SuperAccount.Entity;
using Domain.SuperAccount.Port;
using Domain.SuperAccount.UseCase;
using Infra.Configurations;
using Infra.Context;

using Microsoft.EntityFrameworkCore;

namespace Infra.Account;

public class SuperAccountAdapter(MainDbContext context, IJwtProvider jwtProvider) : SuperAccountPort {
  private readonly MainDbContext context = context;
  private readonly IJwtProvider jwtProvider = jwtProvider;

  public async Task<(string, long)> Authenticate(SuperAccountAuthenticate accountAuthenticate, CancellationToken cancellationToken) {
    var user = await this.context.SuperAccounts
      .Where(x => x.Email == accountAuthenticate.Email)
      .SingleOrDefaultAsync(cancellationToken);

    AccountNotFoundException.ThrowIfNull(user);

    var generatedPassword = Domain.Account.UseCase.AccountAuthenticate.GenerateHash(accountAuthenticate.Password, user.PasswordSalt);

    AccountNotFoundException.ThrowIfFalse(generatedPassword == user.Password);

    var claims = new[]{
      new Claim("sub", user.Id.ToString()),
      new Claim("role", user.GetType().Name),
    };

    var token = this.jwtProvider.Generate(user.Id, claims);
    return (token.Item1, token.Item2);
  }

  public async Task<SuperAccountEntity> CreateAsync(SuperAccountCreate accountCreate, CancellationToken cancellationToken) {
    var accountEntity = SuperAccountEntity.Create(
      id: Guid.NewGuid(),
      name: accountCreate.Name,
      email: accountCreate.Email,
      phone: accountCreate.Phone,
      password: accountCreate.Password,
      passwordSalt: accountCreate.PasswordSalt
    );

    if (accountEntity is null) {
      throw new ArgumentNullException();
    }

    _ = await this.context.SuperAccounts.AddAsync(accountEntity, cancellationToken);
    var changes = await this.context.SaveChangesAsync(cancellationToken);

    if (changes.Equals(0)) {
      throw new DbUpdateException();
    }

    return accountEntity;
  }

  public bool Delete(Guid id) {
    throw new NotImplementedException();
  }

  public async Task<SuperAccountEntity> Retrieve(SuperAccountRetrieve accountRetrieve, CancellationToken cancellationToken) {
    return await this.context.SuperAccounts.FindAsync(accountRetrieve.Id, cancellationToken);
  }

  public async Task<SuperAccountEntity[]> Retrieve(DataRequest accountRetrieve, CancellationToken cancellationToken) {
    return await this.context.SuperAccounts.Take(accountRetrieve.Size).Skip(accountRetrieve.Size * accountRetrieve.Page).ToArrayAsync(cancellationToken);
  }

  public bool Update(Guid id) {
    throw new NotImplementedException();
  }
}
