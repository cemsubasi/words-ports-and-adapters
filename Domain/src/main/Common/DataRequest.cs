using System.Text.Json.Serialization;

namespace Domain.Common;

public class DataRequest {
  private const int DEFAULT_SIZE = 10;

  public int Page { get; private set; }
  public int Size { get; private set; }

  public DataRequest(int page, int size) {
    this.Page = page;
    this.Size = size == 0 ? DEFAULT_SIZE : size;
  }

  public DataRequest Build() {
    return new DataRequest(this.Page, this.Size);
  }
}
