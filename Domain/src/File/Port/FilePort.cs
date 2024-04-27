using Domain.File.Entity;
using Domain.File.UseCase;

namespace Domain.File.Port;

public interface FilePort {
  Task CreateAsync(CreateFile useCase, CancellationToken cancellationToken);
  Task<FileEntity> RetrieveAsync(RetrieveFile useCase, CancellationToken cancellationToken);
  Task<FileEntity[]> RetrieveAsync(RetrieveAllFiles useCase, CancellationToken cancellationToken);
  /* Task<FileEntity> UpdateAsync(UpdateFile useCase, CancellationToken cancellationToken); */
  /* Task<FileEntity> DeleteAsync(DeleteFile useCase, CancellationToken cancellationToken); */
}
