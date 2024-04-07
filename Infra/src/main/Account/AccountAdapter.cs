using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Domain.Account.Entity;
using Domain.Account.Port;
using Domain.Account.UseCase;
using Domain.Common;
using Infra.Configurations;
using Infra.Context;

using Mapster;

using Microsoft.EntityFrameworkCore;

namespace Infra.Account;

public class AccountAdapter : AccountPort {
  private readonly MainDbContext context;
  private readonly IJwtProvider jwtProvider;

  public AccountAdapter(MainDbContext context, IJwtProvider jwtProvider) {
    this.context = context;
    this.jwtProvider = jwtProvider;
  }

  public async Task<(string, long)> Authenticate(AccountAuthenticate accountAuthenticate, CancellationToken cancellationToken) {
    var user = await this.context.Accounts
      .Where(x => x.Email == accountAuthenticate.Email)
      .SingleOrDefaultAsync(cancellationToken);

    AccountNotFoundException.ThrowIfNull(user);

    var generatedPassword = accountAuthenticate.GenerateHash(accountAuthenticate.Password, user.PasswordSalt);

    AccountNotFoundException.ThrowIfFalse(generatedPassword == user.Password);

    var claims = new[]{
      new Claim("sub", user.Id.ToString()),
      new Claim("role", user.GetType().Name),
    };

    var token = this.jwtProvider.Generate(user.Id, claims);
    return (token.Item1, token.Item2);
  }

  public async Task<AccountEntity> CreateAsync(AccountEntity accountEntity, CancellationToken cancellationToken) {
    await this.context.Accounts.AddAsync(accountEntity);
    var changes = await this.context.SaveChangesAsync(cancellationToken);

    if (changes.Equals(0)) {
      throw new DbUpdateException();
    }

    return accountEntity;
  }

  public bool Delete(Guid id) {
    throw new NotImplementedException();
  }

  public async Task<AccountEntity> Retrieve(AccountRetrieve accountRetrieve, CancellationToken cancellationToken) {
    return await this.context.Accounts.FindAsync(accountRetrieve.Id, cancellationToken);
  }

  public async Task<AccountEntity[]> Retrieve(DataRequest accountRetrieve, CancellationToken cancellationToken) {
    return await this.context.Accounts.Take(accountRetrieve.Size).Skip(accountRetrieve.Size * accountRetrieve.Page).ToArrayAsync(cancellationToken);
  }

  public bool Update(Guid id) {
    throw new NotImplementedException();
  }
}
