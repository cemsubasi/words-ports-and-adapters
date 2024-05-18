using Domain.Common;
using Domain.SuperAccount;
using Domain.SuperAccount.Entity;
using Domain.SuperAccount.UseCase;
using Infra.Controllers.SuperAccount;
using Infra.SuperAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infra.Controllers;

[ApiController]
[Authorize(Policy = nameof(SuperAccountEntity))]
[Route("[controller]")]
public class SuperAccountController : ControllerBase {
  private readonly SuperAccountCreateUseCaseHandler accountCreateUseCaseHandler;
  private readonly SuperAccountRetrieveUseCaseHandler accountRetrieveUseCaseHandler;
  private readonly SuperAccountAuthenticateUseCaseHandler accountAuthenticateUseCaseHandler;
  private readonly IUserSession session;

  public SuperAccountController(SuperAccountCreateUseCaseHandler accountCreateUseCaseHandler, SuperAccountRetrieveUseCaseHandler accountRetrieveUseCaseHandler, SuperAccountAuthenticateUseCaseHandler accountAuthenticateUseCaseHandler, IUserSession session) {
    this.accountCreateUseCaseHandler = accountCreateUseCaseHandler;
    this.accountRetrieveUseCaseHandler = accountRetrieveUseCaseHandler;
    this.accountAuthenticateUseCaseHandler = accountAuthenticateUseCaseHandler;
    this.session = session;
  }

  [HttpPost]
  public async Task<IActionResult> Create([FromBody] SuperAccountCreateRequest accountCreateRequest, CancellationToken cancellationToken) {
    var createAccount = await this.accountCreateUseCaseHandler.Handle(accountCreateRequest.ToUseCase(), cancellationToken);

    return this.Ok(SuperAccountCreateResponse.From(createAccount));
  }

  [HttpGet]
  public async Task<IActionResult> Retrieve([FromQuery] Guid id, CancellationToken cancellationToken) {
    var retrieveAccount = await this.accountRetrieveUseCaseHandler.Handle(SuperAccountRetrieve.Build(id), cancellationToken);

    return this.Ok(SuperAccountRetrieveResponse.From(retrieveAccount));
  }

  [HttpPost("all")]
  public async Task<IActionResult> Retrieve([FromBody] DataRequest request, CancellationToken cancellationToken) {
    var accounts = await this.accountRetrieveUseCaseHandler.Handle(request, cancellationToken);

    return this.Ok(SuperAccountRetrieveResponse.From(accounts));
  }

  [AllowAnonymous]
  [HttpPost("login")]
  public async Task<IActionResult> Authenticate([FromBody] SuperAccountAuthenticationRequest accountAuthenticationRequest, CancellationToken cancellationToken) {
    var authenticateAccount = await this.accountAuthenticateUseCaseHandler.Handle(accountAuthenticationRequest.ToUseCase(), cancellationToken);

    this.HttpContext.Response.Headers.Authorization = "Bearer " + authenticateAccount.Item1;
    return this.Ok(SuperAccountAuthenticationResponse.From(authenticateAccount.Item1, authenticateAccount.Item2));
  }
}
