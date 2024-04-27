using Domain.File.Port;
using Domain.File.UseCase;

namespace Domain.File;

public class CreateFileUseCaseHandler(FilePort filePort) {
    private readonly FilePort filePort = filePort;

    public async Task Handle(CreateFile useCase, CancellationToken cancellationToken) {
        await this.filePort.CreateAsync(useCase, cancellationToken);
    }
}
