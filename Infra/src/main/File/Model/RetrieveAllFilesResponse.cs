using Domain.File.Entity;

namespace Domain.File.Model;

public class RetrieveAllFilesResponse {
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }
    public string Name { get; private set; }
    public string Path { get; private set; }
    public string Extension { get; private set; }
    public long Size { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public RetrieveAllFilesResponse(Guid id, Guid accountId, string name, string path, string extension, long size, DateTime createdAt, DateTime? updatedAt) {
        this.Id = id;
        this.AccountId = accountId;
        this.Name = name;
        this.Path = path;
        this.Extension = extension;
        this.Size = size;
        this.CreatedAt = createdAt;
        this.UpdatedAt = updatedAt;
    }

    public static RetrieveAllFilesResponse[] From(FileEntity[] entities) {
        return entities.Select(entity => new RetrieveAllFilesResponse(
            id: entity.Id,
            accountId: entity.AccountId,
            name: entity.Name,
            path: entity.Path,
            extension: entity.Extension,
            size: entity.Size,
            createdAt: entity.CreatedAt,
            updatedAt: entity.UpdatedAt
        )).ToArray();
    }
}
