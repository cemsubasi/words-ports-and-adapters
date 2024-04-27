using Domain.File.Entity;
using Domain.File.Port;
using Domain.File.UseCase;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.File.Adapter;

public class FileAdapter(MainDbContext context) : FilePort {
    private readonly MainDbContext context = context;
    public async Task CreateAsync(CreateFile useCase, CancellationToken cancellationToken) {
        var fileEntity = FileEntity.Create(
            name: useCase.Name,
            path: useCase.Path,
            extension: useCase.Extension,
            contentType: useCase.ContentType,
            size: useCase.Size,
            isVisible: useCase.IsVisible,
            accountId: useCase.AccountId
        );

        await this.context.Files.AddAsync(fileEntity, cancellationToken);
        var result = await this.context.SaveChangesAsync(cancellationToken);
        if (result.Equals(0)) {
            throw new DbUpdateException("Failed to save file");
        }
    }

    public async Task<FileEntity> RetrieveAsync(RetrieveFile useCase, CancellationToken cancellationToken) {
        var fileEntity = await this.context.Files
          .Where(x => x.Id == useCase.Id && x.AccountId == useCase.AccountId)
          .SingleOrDefaultAsync(cancellationToken);

        if (fileEntity == null) {
            throw new KeyNotFoundException("File not found");
        }

        return fileEntity;
    }

    public async Task<FileEntity[]> RetrieveAsync(RetrieveAllFiles useCase, CancellationToken cancellationToken) {
        var fileEntities = await this.context.Files
          .Where(x => x.AccountId == useCase.AccountId)
          .Skip(useCase.Size * useCase.Page)
          .Take(useCase.Size)
          .ToArrayAsync(cancellationToken);

        return fileEntities;
    }
}
