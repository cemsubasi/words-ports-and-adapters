namespace Domain.Common;

public class DataRequest(int page, int size) {
  private const int DEFAULT_SIZE = 10;

  public int Page { get; private set; } = page;
  public int Size { get; private set; } = size == 0 ? DEFAULT_SIZE : size;

  public DataRequest Build() {
    return new DataRequest(this.Page, this.Size);
  }
}
