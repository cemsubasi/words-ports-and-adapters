namespace Domain.Common;

public class DataResponse<T> {
  public List<T> items { get; private set; } = new();
  public uint page { get; private set; }
  public uint size { get; private set; }
  public uint totalSize { get; private set; }

  public DataResponse(List<T> items) {
    this.items = items;
  }

  public DataResponse(List<T> items, uint page, uint size, uint totalSize) {
    this.items = items;
    this.page = page;
    this.size = size;
    this.totalSize = totalSize;
  }

  public static DataResponse<T> Build(List<T> items) {
    return new DataResponse<T>(items);
  }

  public static DataResponse<T> Build(List<T> items, uint page, uint size, uint totalSize) {
    return new DataResponse<T>(items, page, size, totalSize);
  }
}
