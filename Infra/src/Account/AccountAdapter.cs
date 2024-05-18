﻿using System.Security.Claims;
using Domain.Account.Entity;
using Domain.Account.Port;
using Domain.Account.UseCase;
using Domain.Common;
using Infra.Configurations;
using Infra.Context;

using Microsoft.EntityFrameworkCore;

namespace Infra.Account;

public class AccountAdapter(MainDbContext context, IJwtProvider jwtProvider) : AccountPort {
  private readonly MainDbContext context = context;
  private readonly IJwtProvider jwtProvider = jwtProvider;

  public async Task<(string, long)> Authenticate(AccountAuthenticate accountAuthenticate, CancellationToken cancellationToken) {
    var user = await this.context.Accounts
      .Where(x => x.Email == accountAuthenticate.Email)
      .SingleOrDefaultAsync(cancellationToken);

    AccountNotFoundException.ThrowIfNull(user);

    var generatedPassword = AccountAuthenticate.GenerateHash(accountAuthenticate.Password, user.PasswordSalt);

    AccountNotFoundException.ThrowIfFalse(generatedPassword == user.Password);

    var claims = new[]{
      new Claim("sub", user.Id.ToString()),
      new Claim("role", user.GetType().Name),
    };

    var token = this.jwtProvider.Generate(user.Id, claims);
    return (token.Item1, token.Item2);
  }

  public async Task<AccountEntity> CreateAsync(AccountCreate accountCreate, CancellationToken cancellationToken) {
    var accountEntity = AccountEntity.Create(
      id: Guid.NewGuid(),
      email: accountCreate.Email,
      name: accountCreate.Name,
      phone: accountCreate.Phone,
      password: accountCreate.Password,
      passwordSalt: accountCreate.PasswordSalt);

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
