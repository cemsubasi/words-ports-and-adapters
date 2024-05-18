using Domain.File.Entity;
using Domain.File.Port;
using Domain.File.UseCase;

namespace Domain.File;

public class RetrieveFileUseCaseHandler(FilePort filePort) {
    private readonly FilePort filePort = filePort;

    public async Task<FileEntity> Handle(RetrieveFile useCase, CancellationToken cancellationToken) {
        return await this.filePort.RetrieveAsync(useCase, cancellationToken);
    }

    public async Task<FileEntity[]> Handle(RetrieveAllFiles useCase, CancellationToken cancellationToken) {
        return await this.filePort.RetrieveAsync(useCase, cancellationToken);
    }
}