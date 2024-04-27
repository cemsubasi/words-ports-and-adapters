using Domain.File;
using Domain.File.Model;
using Domain.File.UseCase;
using Infra.ass.Operation;
using Infra.File.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Infra.File.Controller;

[Authorize]
[ApiController]
[Route("[controller]")]
public class FileController(
    IUserSession session,
    CreateFileUseCaseHandler createUseCaseHandler,
    RetrieveFileUseCaseHandler retrieveFileUseCaseHandler) : ControllerBase {
    readonly IUserSession session = session;
    readonly CreateFileUseCaseHandler createUseCaseHandler = createUseCaseHandler;

    readonly RetrieveFileUseCaseHandler retrieveFileUseCaseHandler = retrieveFileUseCaseHandler;

    [HttpPost]
    public async Task<IActionResult> Create(FileUploadRequest model, CancellationToken cancellationToken) {
        var fileWriter = new FileWriter();
        var path = await fileWriter.WriteAsync(model.File, cancellationToken);

        await this.createUseCaseHandler.Handle(model.ToUseCase(this.session.Id, path), cancellationToken);

        return this.NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> Retrieve([FromQuery] Guid id, CancellationToken cancellationToken) {
        var result = await this.retrieveFileUseCaseHandler.Handle(RetrieveFile.Build(id, this.session.Id), cancellationToken);

        var fileReader = new FileReader();
        var (file, contentType, fileName) = await fileReader.ReadAsync(result.Path, cancellationToken);

        return this.File(file, contentType, fileName);
    }

    [HttpPost("all")]
    public async Task<IActionResult> Retrieve([FromBody] RetrieveAllFilesRequest request, CancellationToken cancellationToken) {
        var result = await this.retrieveFileUseCaseHandler.Handle(request.ToUseCase(this.session.Id), cancellationToken);

        return this.Ok(RetrieveAllFilesResponse.From(result));
    }
}