using Domain.Account.Entity;

namespace Domain.File.Entity;

public class FileEntity {
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Path { get; set; }
  public string Extension { get; set; }
  public string ContentType { get; set; }
  public long Size { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public Guid AccountId { get; set; }
  public virtual AccountEntity Account { get; set; }
}
