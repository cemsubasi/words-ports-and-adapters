using Domain.Account.Entity;

namespace Domain.File.Entity;

public sealed class FileEntity {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Path { get; set; }
  public string Extension { get; set; }
  public string ContentType { get; set; }
  public long Size { get; set; }
  public bool IsVisible { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }
  public DateTime? DeletedAt { get; set; }
  public Guid AccountId { get; set; }
  public AccountEntity Account { get; set; }

  private FileEntity(string name, string path, string extension, string contentType, long size, bool isVisible, Guid accountId) {
    this.Id = Guid.NewGuid();
    this.Name = name;
    this.Path = path;
    this.Extension = extension;
    this.ContentType = contentType;
    this.Size = size;
    this.IsVisible = isVisible;
    this.CreatedAt = DateTime.UtcNow;
    this.AccountId = accountId;
  }

  public static FileEntity Create(string name, string path, string extension, string contentType, long size, bool isVisible, Guid accountId) {
    return new FileEntity(name, path, extension, contentType, size, isVisible, accountId);
  }
}
