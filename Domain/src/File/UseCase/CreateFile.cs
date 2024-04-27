namespace Domain.File.UseCase;

public class CreateFile {
  public Guid AccountId { get; private set; }
  public string Name { get; private set; }
  public string Path { get; private set; }
  public string Extension { get; private set; }
  public string ContentType { get; private set; }
  public long Size { get; private set; }
  public bool IsVisible { get; private set; }

  private CreateFile(Guid accountId, string name, string path, string extension, string contentType, long size, bool isVisible) {
    this.AccountId = accountId;
    this.Name = name;
    this.Path = path;
    this.Extension = extension;
    this.ContentType = contentType;
    this.Size = size;
    this.IsVisible = isVisible;
  }

  public static CreateFile Build(Guid accountId, string name, string path, string extension, string contentType, long size, bool isVisible) {
    return new CreateFile(accountId, name, path, extension, contentType, size, isVisible);
  }
}
