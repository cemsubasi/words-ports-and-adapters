﻿using Domain.Account;
using Domain.Account.UseCase;
using Domain.Common;

using Infra.Account;
using Infra.Controllers.Account;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase {
  private readonly AccountCreateUseCaseHandler accountCreateUseCaseHandler;
  private readonly AccountRetrieveUseCaseHandler accountRetrieveUseCaseHandler;
  private readonly AccountAuthenticateUseCaseHandler accountAuthenticateUseCaseHandler;
  private readonly IUserSession session;

  public AccountController(AccountCreateUseCaseHandler accountCreateUseCaseHandler, AccountRetrieveUseCaseHandler accountRetrieveUseCaseHandler, AccountAuthenticateUseCaseHandler accountAuthenticateUseCaseHandler, IUserSession session) {
    this.accountCreateUseCaseHandler = accountCreateUseCaseHandler;
    this.accountRetrieveUseCaseHandler = accountRetrieveUseCaseHandler;
    this.accountAuthenticateUseCaseHandler = accountAuthenticateUseCaseHandler;
    this.session = session;
  }

  [AllowAnonymous]
  [HttpPost]
  public async Task<IActionResult> CreateAccount([FromBody] AccountCreateRequest accountCreateRequest, CancellationToken cancellationToken) {
    var createAccount = await this.accountCreateUseCaseHandler.Handle(accountCreateRequest.ToUseCase(), cancellationToken);

    return this.Ok(AccountCreateResponse.From(createAccount));
  }

  [HttpGet]
  public async Task<IActionResult> RetrieveAccount([FromQuery] Guid id, CancellationToken cancellationToken) {
    var retrieveAccount = await this.accountRetrieveUseCaseHandler.Handle(AccountRetrieve.Build(id), cancellationToken);

    return this.Ok(AccountRetrieveResponse.From(retrieveAccount));
  }

  [HttpPost("all")]
  [AllowAnonymous]
  // [Authorize(Policy = "SuperAccountEntity")]
  public async Task<IActionResult> RetrieveAccounts([FromBody] DataRequest request, CancellationToken cancellationToken) {
    var accounts = await this.accountRetrieveUseCaseHandler.Handle(request, cancellationToken);

    return this.Ok(AccountRetrieveResponse.From(accounts));
  }

  [AllowAnonymous]
  [HttpPost("login")]
  public async Task<IActionResult> Authenticate([FromBody] AccountAuthenticationRequest accountAuthenticationRequest, CancellationToken cancellationToken) {
    var authenticateAccount = await this.accountAuthenticateUseCaseHandler.Handle(accountAuthenticationRequest.ToUseCase(), cancellationToken);

    this.HttpContext.Response.Headers.Authorization = "Bearer " + authenticateAccount.Item1;
    return this.Ok(AccountAuthenticationResponse.From(authenticateAccount.Item1, authenticateAccount.Item2));
  }
}
