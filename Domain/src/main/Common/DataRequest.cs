using System.Text.Json.Serialization;

namespace Domain.Common;

public class DataRequest {
  [JsonIgnore]
  public Guid OwnerId { get; private set; }
  public int Page { get; private set; }
  public int Size { get; private set; }

  public DataRequest(Guid ownerId, int page, int size) {
    this.OwnerId = ownerId;
    this.Page = page;
    this.Size = size;
  }

  public DataRequest Build() {
    return new DataRequest(this.OwnerId, this.Page, this.Size);
  }

  public DataRequest Build(Guid ownerId) {
    return new DataRequest(ownerId, this.Page, this.Size);
  }
}
